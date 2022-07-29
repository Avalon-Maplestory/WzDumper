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

namespace WzDumper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:6969/";
            using (var server = CreateWebServer(url))
            {
                // Once we've registered our modules and configured them, we call the RunAsync() method.
                server.RunAsync();

                var browser = new System.Diagnostics.Process() {
                    StartInfo = new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }
                };
                browser.Start();
                // Wait for any key to be pressed before disposing of our web server.
                // In a service, we'd manage the lifecycle of our web server using
                // something like a BackgroundWorker or a ManualResetEvent.
                Console.ReadKey(true);
            }

        }

        // Create and configure our web server.
        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                // First, we will configure our web server by adding Modules.
                .WithLocalSessionManager()
                .HandleHttpException((ctx, ex) => ctx.SendDataAsync(new { status = ex.StatusCode, message = ex.Message }))
                .WithWebApi("/api", m =>
                    m.WithController<WzDumperController>()
                    .WithController<MapDumperController>())
                .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => throw new HttpException(HttpStatusCode.NotFound, ctx.RequestedPath)));

            // Listen for state changes.
            server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();

            return server;
        }
    }

    public class WzDumperController : WebApiController
    {
        [Route(HttpVerbs.Get, "/init")]
        public Task Initialize([QueryData, FormData] NameValueCollection parameters)
        {
            var maplestoryDirectory = parameters["maplestoryDirectory"];
            if (maplestoryDirectory == null)
            {
                throw new HttpException(HttpStatusCode.BadRequest, "missing parameter: maplestoryDirectory");
            }

            WzDumper.Initialize(maplestoryDirectory);
            return Task.CompletedTask;
        }
    }
}
