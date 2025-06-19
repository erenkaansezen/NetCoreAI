using System.Text;
using System.Threading.Channels;
using Newtonsoft.Json;

class Program
{
    private static readonly string apiKey = "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";
    static async Task Main(string[] args)
    {
        Console.WriteLine("Lütfen metini giriniz : ");
        string input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine();
            Console.WriteLine("Duygu analizi yapılıyor..");
            Console.WriteLine();
            string sentiment = await AnalyzaSentiment(input);

            Console.WriteLine($"Sonuç : {sentiment}");
        }
        static async Task<string> AnalyzaSentiment(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "You are an AI that analyzes sentiment. You categorize text as Positive,Negative or Neutral" },
                        new { role = "user", content = $"Analyze the sentiment of this text: {text} and return only Positive,Negative or Neutral" }
                    },

                };

                var json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                string responseJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    return result.choices[0].message.content.ToString();
                }
                else
                {
                    Console.WriteLine("Hatayla karşılaşıldı" + responseJson);
                    return "Hata";
                }
            }
        }
    }

}