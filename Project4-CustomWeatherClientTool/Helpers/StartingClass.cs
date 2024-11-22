namespace Project6
{
    class Starting
    {
        public static void Start(List<City> cities)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Please enter the city for weather information or enter 0 to exit:");
                    string cityName = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(cityName))
                    {
                        Console.WriteLine("City can't be empty.");
                        continue;
                    }

                    if (cityName == "0")
                    {
                        break;
                    }
                    bool isValid = true;
                    foreach (char c in cityName)
                    {
                        if (!(char.IsLetter(c) || char.IsWhiteSpace(c)))
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (!isValid)
                    {
                        Console.WriteLine("Please enter only alphabets");
                        continue;
                    }

                    var city = cities.Find(x =>
                        x.CityName != null &&
                        x.CityName.Equals(cityName, StringComparison.OrdinalIgnoreCase)
                    );

                    if (city == null)
                    {
                        Console.WriteLine("Weather information is not available for the city.");
                    }
                    else
                    {
                        ApiCall.GetWetherData(city).Wait();
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
