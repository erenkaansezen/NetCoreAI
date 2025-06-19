
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Lütfen çevirmek istediğiniz metini giriniz : ");
        string inputText = Console.ReadLine();

        string apiKey = "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"; // Buraya OpenAI API anahtarınızı girin

        string translatedText = await TranslateTextToEnglish(inputText,apiKey);

        if (!string.IsNullOrEmpty(translatedText))
        {
            Console.WriteLine($"Çeviri (EN) : {translatedText}");
        }
        else
        {
            Console.WriteLine("Bir hata oluştu");
        }


    }

    private static async Task<string> TranslateTextToEnglish(string text,string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "You are a helpful translator"
                    },
                    new
                    {
                        role = "user",
                        content = $"Please translate this text to English: {text}"
                    }
                }
            };
            string json = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseString = await response.Content.ReadAsStringAsync();

                dynamic responseObject = JsonConvert.DeserializeObject(responseString);
                string translation = responseObject.choices[0].message.content.ToString();
                return translation;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
                return null;
            }
        }
    }
}