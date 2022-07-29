using EmbedIO.Routing;
using EmbedIO.WebApi;
using EmbedIO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Net;

namespace WzDumper.Map
{
    public class MapController : WebApiController
    {
        [Route(HttpVerbs.Get, "/map/available")]
        public Task<IEnumerable<WzData.AvailableMap>> GetAvailableMaps([QueryData, FormData] NameValueCollection parameters)
        {
            var verifyExists = parameters.GetParameter("verifyExists", "false");
            var count = parameters.GetParameter("count", "-1");
            var offset = parameters.GetParameter("offset", "0");

            IEnumerable<WzData.AvailableMap> result = WzDumper.Instance.GetAvailableMaps(bool.Parse(verifyExists), int.Parse(count), int.Parse(offset));
            return Task.FromResult(result);
        }

        [Route(HttpVerbs.Get, "/map/dump")]
        public Task<WzData.MapData> Dump([QueryData, FormData] NameValueCollection parameters)
        {
            var mapId = parameters.GetParameter("mapId");
            var assetsDirectory = parameters.GetParameter("assetsDirectory");
            
            var (mapData, assets) = WzDumper.Instance.DumpMap(int.Parse(mapId));
            assets.SaveTo(assetsDirectory);

            return Task.FromResult(mapData);
        }
    }
}
