using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.IO;
using System.Runtime.Serialization.Json;
using WeatherApp;
using static WeatherApp.WeatherInfo;
using System.Data;

namespace WeatherApp
{
    class CurrentWeather
    {
        private WeatherInfo.Root root;

        public CurrentWeather()
        {
            root = new WeatherInfo.Root();
        }
        public void GetCurrentWeather(string city)
        {
            string jsonCurrentWeather = AccessWebPage.HttpGet(city);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(WeatherInfo.Root));

            using (Stream s = GenerateStreamFromString(jsonCurrentWeather))
            {

                root = (WeatherInfo.Root)ser.ReadObject(s);
            }
            

        }
        public void ShowWeather()
        {
            Console.WriteLine("{0}, godz.{1}:{2}", root.city.name,LocalTime(),DateTime.Now.Minute);
            Console.WriteLine("Aktualna temperatura: {0} C\n", root.list[0].main.temp);
        
        }
        public void ShowForecast()
        {
            Console.WriteLine("Prognoza pogody do końca dnia");
            int cnt = CountCnt();
            for (int i = 0; i < cnt; i++)
            {
                var parsedDate = DateTime.Parse(root.list[i].dt_txt);
                int time = LocalTime(parsedDate.Hour);
                int endTime;
                if (time+3 > 24)
                     endTime = 24;              
                else                
                    endTime = time + 3;
                
                Console.WriteLine("\ntime:({0} - {1})", time, endTime);
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

        public int LocalTime(int h = -1)
        {
            if(h < 0)
                h = DateTime.UtcNow.Hour;

            int time = h + (root.city.timezone / 3600);
            if(time > 24)
            {
                return time - 24;
            }
            if(time < 0)
            {
                return 24 + time;
            }
            return time;
        }
        public int CountCnt() { 
            int temp = 24 - LocalTime();
            if(temp == 0)
            {
                return 8;
            }
            return temp / 3;
        }

        public static string DegreesToCardinal(double degrees)
        {
            degrees *= 10;

            string[] caridnals = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" };
            return caridnals[(int)Math.Round(((double)degrees % 3600) / 225)];
        }

    }
}
