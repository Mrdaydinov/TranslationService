
using Grpc.Core;
using Grpc.Net.Client;

namespace TranslationGrpcClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7018");

            var client = new Translator.TranslatorClient(channel);

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


                TranslationMessage translationMessage = new TranslationMessage
                {
                    Text = text,
                    SourceLanguage = sourceLanguage,
                    TargetLanguage = targetLanguage
                };

                try
                {
                    var translatedMessage = client.Translate(translationMessage);
                    Console.WriteLine($"Translated text: {translatedMessage.TranslatedText}");
                }
                catch (RpcException e)
                {
                    Console.WriteLine($"Error during translation: {e.Status.Detail}");
                }

                var infoRequest = new VoidRequest();

                try
                {
                    var info = client.GetInfo(infoRequest);
                    Console.WriteLine("\n" + info.Info + "\n"); ;
                }
                catch (RpcException e)
                {
                    Console.WriteLine($"Error while getting service information: {e.Status.Detail}");
                }
            }
        }
    }
}
