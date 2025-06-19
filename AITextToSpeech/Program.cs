using Newtonsoft.Json;
using System.Text;

class Program
{
    //private static readonly string apiKey = "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";
    static async Task Main(string[] args)
    {
        Console.Write("Metni Giriniz: ");
        string input;
        input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Ses dosyası oluşuturuluyor....");
            await GenerateSpeech(input);
            Console.Write("Ses dosyası 'output mp3' olarak kaydedildi!");
            System.Diagnostics.Process.Start("explorer.exe", "output.mp3");

        }

        static async Task GenerateSpeech(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "tts-1",
                    input = text,
                    voice = "Sky"
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/audio/speech", content);

                if (response.IsSuccessStatusCode)
                {
                    byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync("output.mp3", audioBytes);
                }
                else
                {
                    Console.WriteLine("Bir hata oluştu");
                }

            }
        }
    }
}