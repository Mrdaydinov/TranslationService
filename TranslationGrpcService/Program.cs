using TranslationGrpcService.Services;
using TranslationService;

namespace TranslationGrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();
            builder.Services.AddSingleton<ITranslateService, TranslateService>();

            var app = builder.Build();

            app.MapGrpcService<TranslatorService>();
            app.MapGet("/", () => "");

            app.Run();
        }
    }
}