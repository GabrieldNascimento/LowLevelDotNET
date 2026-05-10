using System.Net;

namespace LowLevelDotNET.Routing
{
    public class RequestContext
    {
        public HttpListenerContext Context {get;}
        public Dictionary<string, string> RouteParams {get;} = new();

        // Construtor recebe o contexto
        public RequestContext(HttpListenerContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetRouteValue(string key)
        {
            return RouteParams.TryGetValue(key, out var value) ? value : null;
        }

        // Opcional: pegar query string (ex: ?foo=bar)
        public string GetQueryValue(string key)
        {
            var query = Context.Request.QueryString;
            return query[key];
        }
        public async Task WriteStringAsync(string content, int statusCode = 200)
        {
            await HttpResponseHelper.SendStringAsync(Context, content, statusCode);
        }

        public async Task SendJsonAsync<T>(HttpListenerContext context, T obj, int statusCode = 200)
        {
            await HttpResponseHelper.SendJsonAsync(Context, obj, statusCode);
        }
    }
}