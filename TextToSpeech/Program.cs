
using System.Speech.Synthesis;

class Program
{
    static void Main(string[] args)
    {
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        synthesizer.Volume = 100; // Sesin yüksekliği 0-100 arasında ayarlanabilir
        synthesizer.Rate = -2; // Sesin hızı -10 ile 10 arasında ayarlanabilir

        Console.WriteLine("Metini Giriniz : ");
        string inputText = Console.ReadLine();
        if (!string.IsNullOrEmpty(inputText))
        {
            synthesizer.Speak(inputText);
            Console.WriteLine("Metin okundu.");
        }
        else
        {
            Console.WriteLine("Lütfen geçerli bir metin giriniz.");
        }
        Console.ReadLine();

    }
}