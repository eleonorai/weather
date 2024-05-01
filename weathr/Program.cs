using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

Console.WriteLine("Enter the name of the city:");
string city = Console.ReadLine();

string apiKey = "ec2eca8daee3049d48ff1fc483dfe7f5";

string urlForecast = string.Format($"http://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric&lang=en");
using (WebClient web = new WebClient())
{
    var jsonForecast = web.DownloadString(urlForecast);
    var dataForecast = JObject.Parse(jsonForecast);

    Console.WriteLine($"Weather for the next 7 days in the city {city}:");
    foreach (var item in dataForecast["list"])
    {
        var date = DateTime.Parse(item["dt_txt"].ToString());
        var localTime = TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Utc, TimeZoneInfo.Local);
        var temperature = item["main"]["temp"];
        var weatherDescription = item["weather"][0]["description"];

        Console.WriteLine($"Date: {localTime.ToShortDateString()}\tTime: {localTime.ToShortTimeString()}\tTemperature: {temperature}°C\tWeather description: {weatherDescription}");
    }
}