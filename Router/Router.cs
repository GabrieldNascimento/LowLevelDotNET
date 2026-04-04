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


        //Ok, então vou precisar de um método que vai adicionar itens a minha lista de rotas, pasasndo o verbo http, path e o handler
        public void MapGet(string path, Func<HttpListenerContext, Task> handler)
        {
            _routes.Add(new Route("GET", path, handler));
        }

        public void MapPost(string path, Func<HttpListenerContext, Task> handler)
        {
            _routes.Add(new Route("POST", path, handler));
        }

         public void Use(MiddlewareDelegate middleware)
        {
            _middlewares.Add(middleware);
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
            var path = context.Request.Url.AbsolutePath;
            var method = context.Request.HttpMethod;
            
            var route = _routes.Find(r => r.Path == path && r.Method == method);

            if(route is not null)
            {
                await route.Handler(context);
                return;
            }

            // 404
            await HttpResponseHelper.SendStringAsync(context, "404 - Not Found", 404);
        }
    }   
}