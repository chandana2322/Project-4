using System.Text.Json;

namespace Project6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                string filePath = @"D:\PracticeProjects\Project4-CustomWeatherClientTool\CityConstants.json";

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("file not found.");
                    return;
                }

                string jsonString = File.ReadAllText(filePath);
                var cities = JsonSerializer.Deserialize<List<City>>(jsonString);
                if (cities == null || cities.Count == 0)
                {
                    Console.WriteLine("no cities found.");
                    return;
                }

                Starting.Start(cities);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
