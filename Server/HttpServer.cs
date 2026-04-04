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
                Console.WriteLine($"Recebi uma requisição: {context.Request.HttpMethod} {context.Request.Url}");

                string responseString = "Oi, isso é um teste!";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
            }
        }   
    }
}