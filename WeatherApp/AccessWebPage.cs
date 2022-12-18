using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    class AccessWebPage
    {
        public static string HttpGet(string url)
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
