using EmbedIO.Routing;
using EmbedIO.WebApi;
using EmbedIO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzDumper.Map
{
    public class MapDumperController : WebApiController
    {
        [Route(HttpVerbs.Get, "/map/available")]
        public Task<IEnumerable<WzData.AvailableMap>> GetAvailableMaps()
        {
            IEnumerable<WzData.AvailableMap> result = WzDumper.Instance.GetAvailableMaps();
            return Task.FromResult(result);
        }
    }
}
