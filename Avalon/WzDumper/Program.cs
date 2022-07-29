using EmbedIO.Actions;
using EmbedIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System.Collections.Specialized;
using Swan.Cryptography;
using System.Net;
using WzDumper.Map;
using Swan.Logging;
using System.IO;
using System.Threading;

namespace WzDumper
{
    internal class Program
    {
        public static ManualResetEvent ShutdownEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine($"Usage: {Environment.GetCommandLineArgs()[0]} port");
            }

            string url = $"http://localhost:{int.Parse(args[0])}/";
            using (var server = CreateWebServer(url))
            {
                server.RunAsync();
                ShutdownEvent.WaitOne();
            }
        }

        static async Task Shutdown(IHttpContext ctx)
        {
            await ctx.SendDataAsync(new { status = HttpStatusCode.OK, message = (string)null });
            ShutdownEvent.Set();
        }

        // Create and configure our web server.
        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                // First, we will configure our web server by adding Modules.
                .WithLocalSessionManager()
                .HandleUnhandledException(ExceptionHandler.DataResponseForException)
                .HandleHttpException(ExceptionHandler.DataResponseForHttpException)
                .WithWebApi("/api", m =>
                    m.WithController<WzDumperController>()
                    .WithController<MapController>())
                .WithModule(new ActionModule("/shutdown", HttpVerbs.Any, Shutdown))
                .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => throw new HttpException(HttpStatusCode.NotFound, ctx.RequestedPath)));

            // Listen for state changes.
            server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();

            return server;
        }

        internal static class ExceptionHandler
        {
            private static Dictionary<Type, HttpStatusCode> EXCEPTION_STATUS_CODE_MAPPING = new Dictionary<Type, HttpStatusCode>(){
                [typeof(InvalidOperationException)] = HttpStatusCode.BadRequest,
                [typeof(DirectoryNotFoundException)] = HttpStatusCode.NotFound,
                [typeof(KeyNotFoundException)] = HttpStatusCode.NotFound,
            };

            public static Task DataResponseForException(IHttpContext context, Exception exception)
            {
                if (EXCEPTION_STATUS_CODE_MAPPING.TryGetValue(exception.GetType(), out HttpStatusCode status))
                {
                    context.Response.StatusCode = (int)status;
                    throw new HttpException(status, exception.Message);
                }

                throw new HttpException(HttpStatusCode.InternalServerError, exception.Message);
            }

            public static Task DataResponseForHttpException(IHttpContext context, IHttpException httpException)
            {
                return ResponseSerializer.Json(context, new { status = httpException.StatusCode, message = httpException.Message });
            }
        }
    }
}
