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
using SharpDX;
using WzDumper.WzData;

namespace WzDumper.Map
{
    public class MapController : WebApiController
    {
        [Route(HttpVerbs.Get, "/map/available")]
        public object GetAvailableMaps([QueryData, FormData] NameValueCollection parameters)
        {
            var query = parameters.GetParameter("query", ".*");
            var verifyExists = parameters.GetParameter("verifyExists", "false");
            var count = parameters.GetParameter("count", "-1");
            var offset = parameters.GetParameter("offset", "0");

            var availableMaps = new WzData.Map.AvailableMaps() {
                maps = WzDumper.Instance.GetAvailableMaps(query, bool.Parse(verifyExists), int.Parse(count), int.Parse(offset))
            };

            return new {
                status = HttpStatusCode.OK,
                data = availableMaps
            };
        }

        [Route(HttpVerbs.Get, "/map/dump")]
        public object Dump([QueryData, FormData] NameValueCollection parameters)
        {
            var mapId = parameters.GetParameter("mapId");
            var assetsDirectory = parameters.GetParameter("assetsDirectory");
            
            var (mapData, bitmaps) = WzDumper.Instance.DumpMap(int.Parse(mapId));
            MapDataSaver.SaveBitmaps(bitmaps, assetsDirectory);

            return new {
                status = HttpStatusCode.OK,
                data = mapData
            };
        }
    }
}
