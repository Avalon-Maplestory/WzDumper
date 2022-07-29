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
        public Task Initialize([QueryData, FormData] NameValueCollection parameters)
        {
            var maplestoryDirectory = parameters.GetParameter("maplestoryDirectory");

            WzDumper.Initialize(maplestoryDirectory);
            throw new HttpException(HttpStatusCode.OK);
        }

        [Route(HttpVerbs.Get, "/uninitialize")]
        public Task Initialize()
        {
            WzDumper.Uninitialize();
            throw new HttpException(HttpStatusCode.OK);
        }
    }
}
