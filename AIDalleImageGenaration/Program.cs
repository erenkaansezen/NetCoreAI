
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        string apiKey = "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";
        Console.WriteLine("Example Prompts : ");
        string prompt = Console.ReadLine();
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                model = "dall-e-3",
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody,Encoding.UTF8,"application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);

            string responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
    }
}