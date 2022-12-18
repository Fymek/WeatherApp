using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using WeatherApp;

namespace WeatherApp
{
    class CurrentWeather
    {
        public void GetCurrentWeather()
        {
            Console.Write("Podaj nazwe miasta: ");
            string city = Console.ReadLine();
            string jsonCurrentWeather = AccessWebPage.HttpGet("http://api.openweathermap.org/data/2.5/weather?q=" + city + "&APPID=9247303b46ab8e34e80eb533aa513f23");
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(WeatherInfo.Root));
         
            WeatherInfo.Root root = new WeatherInfo.Root();

            using (Stream s = GenerateStreamFromString(jsonCurrentWeather))
            {

                root = (WeatherInfo.Root)ser.ReadObject(s);
            }
            
            
            Console.WriteLine("Temperatura wynosi {0})", root.main.temp);//todo zmienic na celsjusze

            //Console.WriteLine("");
            //Console.WriteLine(jsonCurrentWeather);
            //Console.ReadLine();
        }

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [DataContract]
        internal class WeatherNow
        {

            [DataMember]
            public WeatherInfo.Coord coord { get; set; }
            [DataMember]
            public WeatherInfo.Sys sys { get; set; }
            [DataMember]
            public List<WeatherInfo.Weather> weather { get; set; }
            [DataMember]
            public string @base { get; set; }
            [DataMember]
            public WeatherInfo.Main main { get; set; }
            [DataMember]
            public WeatherInfo.Wind wind { get; set; }
            [DataMember]
            public Dictionary<string, double> rain { get; set; }
            [DataMember]
            public WeatherInfo.Clouds clouds { get; set; }
            [DataMember]
            public int dt { get; set; }
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public int cod { get; set; }

        }

    }
}
