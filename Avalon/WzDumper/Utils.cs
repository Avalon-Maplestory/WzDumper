using EmbedIO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WzDumper
{
    public static class Utils
    {
        public static string GetParameter(this NameValueCollection parameters, string parameter)
        {
            var value = parameters[parameter];
            if (value == null)
            {
                throw new InvalidOperationException("missing parameter: mapId");
            }
            return value;
        }

        public static string GetParameter(this NameValueCollection parameters, string parameter, string default_value)
        {
            var value = parameters[parameter];
            if (value == null)
            {
                if (default_value == null)
                {
                    throw new InvalidOperationException("missing parameter: mapId");
                }
                return default_value;
            }
            return value;
        }
    }
}
