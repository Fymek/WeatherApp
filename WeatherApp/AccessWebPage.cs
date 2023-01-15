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
        private const string ApiUrlBase = "http://api.openweathermap.org/data/2.5/forecast?q=";
        private const string UnitParameter = "units=metric";
        private const string TimeStaps = "cnt=9"; // MAKS 40 FOR FREE OPENWEATHER API

        private const string ApiKey = "9247303b46ab8e34e80eb533aa513f23";
        public static string HttpGet(string city)
        {
            string url = BuildUrlWithParams(city);
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }

        private static string BuildUrlWithParams(string city)
        {
            string UrlRequest = ApiUrlBase+city+"&APPID="+ApiKey+"&"+UnitParameter+"&"+TimeStaps;
            return UrlRequest;
        }
    }
}
