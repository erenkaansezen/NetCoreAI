
using Google.Cloud.Vision.V1;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Resim Yolunu Giriniz");
        string imgPath = Console.ReadLine();
        Console.WriteLine();

        //string credentialPath = @"C:\Users\eren.sezen\Desktop\NetCoreAI\GoogleCloudVision\profound-force-463321-a7-e2f46ed4afa0.json"; // buraya servis json dosyası gelicek
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

        try
        {
            var client = ImageAnnotatorClient.Create();

            var image = Image.FromFile(imgPath);
            var response = client.DetectText(image);
            Console.WriteLine("Resimdeki Metin : ");
            Console.WriteLine();
            foreach (var annotation in response)
            {
                if (!string.IsNullOrEmpty(annotation.Description))
                {
                    Console.WriteLine(annotation.Description);
                }
            }
        }
        catch
        (Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }
}