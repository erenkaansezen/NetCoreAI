
using System.Net.Http.Headers;

class Program
{
    static async Task Main(string[] args)
    {
        //string apiKey = "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";
        string audioFilePath = "ismailbas.mp3";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var form = new MultipartFormDataContent();

            var audioContent = new ByteArrayContent(File.ReadAllBytes(audioFilePath));
            audioContent.Headers.ContentType = MediaTypeHeaderValue.Parse("audio/mpeg");
            form.Add(audioContent, "file", Path.GetFileName(audioFilePath));
            form.Add(new StringContent("whisper-1"), "model");

            Console.WriteLine($"Merhaba! {audioFilePath} İsimli Ses Dosyası İşleniyor.");
            
            var response = await client.PostAsync("https://api.openai.com/v1/audio/transcriptions", form);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Ses dosyasının yazılı hali : ");
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Hata{response.StatusCode }");
            }

            Console.ReadLine();
        }
    }
}