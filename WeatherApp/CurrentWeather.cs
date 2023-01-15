using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using WeatherApp;
using static WeatherApp.WeatherInfo;

namespace WeatherApp
{
    class CurrentWeather
    {
        private WeatherInfo.Root root;
        public void GetCurrentWeather(string city)
        {
            string jsonCurrentWeather = AccessWebPage.HttpGet(city);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(WeatherInfo.Root));
         
            root = new WeatherInfo.Root();

            using (Stream s = GenerateStreamFromString(jsonCurrentWeather))
            {

                root = (WeatherInfo.Root)ser.ReadObject(s);
            }
            

        }
        public void ShowWeather()
        {
            Console.WriteLine("Aktualna temperatura: {0} C\n", root.list[0].main.temp);
        }
        public void ShowForecast()
        {
            int cnt = CountCnt();
            for (int i = 0; i < cnt; i++)
            {
                Console.WriteLine("\n time: {0}", root.list[i].dt_txt);
                Console.WriteLine("Maksymalna temperatura: {0} C", root.list[i].main.temp_max);
                Console.WriteLine("Minimalna temperatura: {0} C", root.list[i].main.temp_min);
                if (root.list[i].rain == null)
                    Console.WriteLine("Ilość opadów: 0mm");
                else
                    Console.WriteLine("Ilość opadów: {0}mm", root.list[i].rain.threeh);
                Console.WriteLine("Wiatr:{0} m/s {1} ({2})", root.list[i].wind.speed, DegreesToCardinal(root.list[i].wind.deg), root.list[i].wind.deg);
            }
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

        public int CountCnt()
        {
            int h = DateTime.Now.Hour;
            
            return (24-h) / 3;
        }

        public static string DegreesToCardinal(double degrees)
        {
            degrees *= 10;

            string[] caridnals = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" };
            return caridnals[(int)Math.Round(((double)degrees % 3600) / 225)];
        }

    }
}
