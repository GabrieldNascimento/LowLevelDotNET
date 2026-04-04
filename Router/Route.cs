using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LowLevelDotNET.Routing
{
    public class Route
    {
        public string Path {get;}
        public string Method {get;}
        public Func<HttpListenerContext, Task> Handler {get;}

        public Route(string path, string method, Func<HttpListenerContext, Task> handler)
        {
            Path = path;
            Method = method;
            Handler = handler;
        }

    }
}