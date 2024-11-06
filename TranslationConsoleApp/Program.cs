using DeepL.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Emit;
using TranslationService;

namespace TranslationConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using var serviceProvider = new ServiceCollection().AddSingleton<ITranslateService>(new TranslateService()).BuildServiceProvider();

            var translateService = serviceProvider.GetService<ITranslateService>();


            while (true)
            {
                Console.WriteLine("Enter source language (type code or type '0' to quit):\n");
            sourceLanguage:
                string sourceLanguage = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(sourceLanguage))
                {
                    Console.WriteLine("Source language cannot be empty, type again");
                    goto sourceLanguage;
                }
                else if (sourceLanguage == "0")
                    break;


                Console.WriteLine("\n==========");


                Console.WriteLine("\nEnter target language (type code or type '0' to quit):\n");
            targetLanguage:
                string targetLanguage = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(targetLanguage))
                {
                    Console.WriteLine("Target language cannot be empty, type again");
                    goto targetLanguage;
                }
                else if (targetLanguage == "0")
                    break;


                Console.WriteLine("\n==========");


                Console.WriteLine("Type your text (or type '0' to quit)\n");
            text:
                string text = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(targetLanguage))
                {
                    Console.WriteLine("Text cannot be empty, type again");
                    goto text;
                }
                else if (text == "0")
                    break;

                try
                {
                    string translatedText = await translateService.TranslateAsync(text, sourceLanguage, targetLanguage);
                    Console.WriteLine("\n" + $"Translated text: {translatedText}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError during translation: {ex.Message}");
                }


                Console.WriteLine("==========");

                Console.WriteLine(translateService.GetServiceInfo() + "\n");
            }
        }
    }
}