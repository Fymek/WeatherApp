﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Program
    {
        static void Main(String[] args)
        {
            CurrentWeather weatherNow = new CurrentWeather();
            weatherNow.GetCurrentWeather();
        }
    }
    
}
