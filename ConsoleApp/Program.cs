using System.Text;
using Newtonsoft.Json;

namespace ConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var prompt = "Was kannst du?";
        
        // Erstelle die HTTP-Request-Meldung
        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:11434/api/generate");
        
        // Füge den Accept-Header hinzu
        request.Headers.Add("Accept", "application/json");

        // Erstelle den JSON-Inhalt und setze die Content-Type-Header über das StringContent-Objekt
        var content = new StringContent(
            JsonConvert.SerializeObject(new
            {
                model = "deepseek-r1:32b",
                prompt = prompt,
                stream = false
            }), 
            Encoding.UTF8, 
            "application/json"
        );
        
        // Setze den Inhalt der Anfrage
        request.Content = content;

        try
        {
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Error: {await response.Content.ReadAsStringAsync()}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        Console.WriteLine("Fertig!");
    }
}