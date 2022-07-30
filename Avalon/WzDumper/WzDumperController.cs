using EmbedIO.Routing;
using EmbedIO.WebApi;
using EmbedIO;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;

namespace WzDumper
{
    public class WzDumperController : WebApiController
    {
        [Route(HttpVerbs.Get, "/initialize")]
        public object Initialize([QueryData, FormData] NameValueCollection parameters)
        {
            var maplestoryDirectory = parameters.GetParameter("maplestoryDirectory");

            WzDumper.Initialize(maplestoryDirectory);
            return new {
                status = HttpStatusCode.OK,
                data = new { }
            };
        }

        [Route(HttpVerbs.Get, "/uninitialize")]
        public object Uninitialize()
        {
            WzDumper.Uninitialize();
            return new {
                status = HttpStatusCode.OK,
                data = new { }
            };
        }
    }
}
