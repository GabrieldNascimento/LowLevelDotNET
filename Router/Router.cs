using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LowLevelDotNET.Routing
{
    public class Router
    {        
        private readonly List<Route> _routes = new();
        private readonly List<MiddlewareDelegate> _middlewares = new();

        public void MapGet(string path, Func<RequestContext, Task> handler)
        {
            _routes.Add(new Route("GET", path, handler));
        }

        public void MapPost(string path, Func<RequestContext, Task> handler)
        {
            _routes.Add(new Route("POST", path, handler));
        }
         public void Use(MiddlewareDelegate middleware)
        {
            _middlewares.Add(middleware);
        }

        public bool TryMatch(Route route, string requestPath, out Dictionary<string, string> parameters)
        {   
            parameters = new Dictionary<string, string>();

            var routeSegments = route.Path.Split("/", StringSplitOptions.RemoveEmptyEntries); 
            var requestSegments = requestPath.Split("/", StringSplitOptions.RemoveEmptyEntries);

            if(routeSegments.Length != requestSegments.Length)
            {
                return false;
            }

            for(int i =0; i < routeSegments.Length; i++)
            {
                var rSeg = routeSegments[i]; 
                var reqSeg = requestSegments[i];
                if(rSeg.StartsWith("{") && rSeg.EndsWith("}"))
                {
                   var paramName =  rSeg[1..^1]; //Range Syntax
                    parameters[paramName] = reqSeg;
                }else if (!rSeg.Equals(reqSeg, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task HandleAsync(HttpListenerContext context)
        {
            int index = -1;

            Func<Task> next = null;

            next = async () =>
            {
                index++;
                if (index < _middlewares.Count)
                {
                    await _middlewares[index](context, next);
                }
                else
                {
                    await HandleRouteAsync(context);
                }
            };

            await next();
        }

        public async Task HandleRouteAsync( HttpListenerContext context)
        {
            var path = context.Request.Url.AbsolutePath;//
            var method = context.Request.HttpMethod;
            
            foreach(var route in _routes)
            {
                if(route.Method != context.Request.HttpMethod) continue;

                if(TryMatch(route, path, out var parameters))
                {
                    var reqCtx = new RequestContext(context);
                    foreach(var kv in parameters)
                        reqCtx.RouteParams[kv.Key] = kv.Value;

                    await route.Handler(reqCtx); // Handler agora recebe RequestContext
                    return;
                }
            }

            // 404
            await HttpResponseHelper.SendStringAsync(context, "404 - Not Found", 404);
        }
    }   
}