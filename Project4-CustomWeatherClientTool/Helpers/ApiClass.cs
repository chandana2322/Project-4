using System.Text;
using System.Text.Json;

namespace Project6
{
    class ApiCall
    {
        public static async Task GetWetherData(City city)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={city.Lat}&longitude={city.Lng}&current_weather=true";

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        JsonDocument wetherData = JsonDocument.Parse(content);
                        var currentWeatherUnits = wetherData.RootElement.GetProperty("current_weather_units");
                        var currentWeather = wetherData.RootElement.GetProperty("current_weather");
                        DateTime gmtTime = DateTime.Parse(currentWeather.GetProperty("time").GetString());
                        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(gmtTime, TimeZoneInfo.Local);
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine($"Time: {localTime.ToString("yyyy-MM-dd HH:mm:ss")}");
                        stringBuilder.AppendLine($"Temperature: {currentWeather.GetProperty("temperature")}{currentWeatherUnits.GetProperty("temperature")}");
                        stringBuilder.AppendLine($"Windspeed: {currentWeather.GetProperty("windspeed")}{currentWeatherUnits.GetProperty("windspeed")}");
                        stringBuilder.AppendLine($"winddirection: {currentWeather.GetProperty("winddirection")}{currentWeatherUnits.GetProperty("winddirection")}");
                        Console.WriteLine(stringBuilder);
                    }
                    else
                    {
                        Console.WriteLine(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
