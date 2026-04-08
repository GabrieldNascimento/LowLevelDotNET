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

        // Helper para pegar valor de rota com segurança
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
        //Não coloquei o helper de resposta, pq já tenho em outra classe
    }
}