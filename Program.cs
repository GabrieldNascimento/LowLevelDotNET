using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LowLevelDotNET.Routing;
using LowLevelDotNET.Server;

namespace LowLevelDotNET
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var router = new Router();
            var server = new HttpServer(router);
            await server.StartAsync("http://localhost:5000/");
        }
    }
}