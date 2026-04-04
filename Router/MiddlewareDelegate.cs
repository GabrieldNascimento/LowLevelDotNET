using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LowLevelDotNET.Routing
{
    public delegate Task MiddlewareDelegate(HttpListenerContext context, Func<Task> next); 
}