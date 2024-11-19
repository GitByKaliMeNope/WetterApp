using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Wetter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Für welche Stadt möchen Sie das Wetter wissen?");

                string city = Console.ReadLine();

                using HttpClient httpClient = new HttpClient();

                // API-URL mit korrektem Parameter
                string requestUri = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=17da989e3d3c70a730c374476fe73f67&units=metric";

                // Sende die Anfrage
                HttpResponseMessage httpResponse = await httpClient.GetAsync(requestUri);

                // Überprüfe, ob die Anfrage erfolgreich war
                if (httpResponse.IsSuccessStatusCode)
                {
                    string response = await httpResponse.Content.ReadAsStringAsync();

                  

                    // JSON-Daten deserialisieren
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(response);


                    if (weatherData != null && weatherData.Main != null)
                    {
                        // Ausgabe der Temperatur
                        Console.WriteLine($"Die aktuelle Temperatur in {weatherData.Name} beträgt {weatherData.Main.Temp}°C.");
                    }
                    else
                    {
                        Console.WriteLine("Fehler beim Verarbeiten der Wetterdaten.");
                    }
                }
                else
                {
                    Console.WriteLine($"Fehler: {httpResponse.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }
        }
    }

    // Klassen für die JSON-Daten
    public class WeatherData
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
    }

    public class MainData
    {
        public float Temp { get; set; }
    }
}
