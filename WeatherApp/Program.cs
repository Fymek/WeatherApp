using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static WeatherApp.WeatherInfo;

namespace WeatherApp
{
    class Program
    {
        
        private static string LoadCity()
        {
            string text = File.ReadAllText("save.txt");
            Console.WriteLine(text);
            if(text.Count() > 0)
                return text;
            return null;
        }
        private static async Task SaveCity(string city)
        {
            Console.WriteLine("\nUstawić miasto jako domysle? (tak/nie)");
            string ans = Console.ReadLine();
            if(ans.ToLower().CompareTo("tak")==0)
                await  File.WriteAllTextAsync("save.txt", city);
        }
        private static string ReadCity()
        {
            Console.Write("Podaj nazwe miasta: ");
            string city = Console.ReadLine();

            return city;
        }
        
        static void Main(String[] args)
        {
            string city = LoadCity();
            if(city == null)
            {
                city = ReadCity();
            }
            CurrentWeather weatherNow = new CurrentWeather();
            while (true)
            {
                //Console.Clear();
                
                weatherNow.GetCurrentWeather(city);
                weatherNow.ShowWeather();
                weatherNow.ShowForecast();
                SaveCity(city);
                city=ReadCity();
            }
            
        }
    }
    
}
