using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WeatherApp
{
    class Program
    {
        
        private static string LoadCity()
        {
            string text = File.ReadAllText("save.txt");
            Console.WriteLine(text);
            return null;
        }
        private static async Task SaveCity(string city)
        {
         await  File.WriteAllTextAsync("save.txt", city);
        }
        //ogarnac ui 
        static void Main(String[] args)
        {
            string city = LoadCity();
            if(city == null)
            {
                Console.Write("Podaj nazwe miasta: ");
                city = Console.ReadLine();
            }
            SaveCity(city);
            CurrentWeather weatherNow = new CurrentWeather();
            weatherNow.GetCurrentWeather(city);
            weatherNow.ShowWeather();
            weatherNow.ShowForecast();
            
        }
    }
    
}
