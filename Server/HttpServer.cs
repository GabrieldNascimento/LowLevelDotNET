using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LowLevelDotNET.Routing;

namespace LowLevelDotNET.Server
{
    public class HttpServer
    {
        private readonly HttpListener _listener;
        private readonly Router _router;

        public HttpServer(Router router)
        {
            _listener = new HttpListener();
            _router = router;
        }

        public async Task StartAsync(string prefix)
        {
            _listener.Prefixes.Add(prefix);
            _listener.Start();
            Console.WriteLine($"Servidor rodando em: {prefix}");

            while (true)
            {
                var context = await _listener.GetContextAsync();
                Console.WriteLine($"Requisição Recebida: {context.Request.HttpMethod} {context.Request.Url}");

                await _router.HandleAsync(context);
            }
        }   
    }
}