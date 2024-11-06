using Grpc.Core;
using TranslationService;

namespace TranslationGrpcService.Services
{
    public class TranslatorService : Translator.TranslatorBase
    {
        private readonly ITranslateService _translateService;

        public TranslatorService(ITranslateService translateService)
        {
            _translateService = translateService;
        }

        public override async Task<TranslatedMessage> Translate(TranslationMessage request, ServerCallContext context)
        {
            var text = request.Text;
            var sourceLanguage = request.SourceLanguage;
            var targetLanguage = request.TargetLanguage;

            var translatedText = await _translateService.TranslateAsync(text, sourceLanguage, targetLanguage);

            return new TranslatedMessage
            {
                TranslatedText = translatedText
            };
        }

        public override Task<ServiceInfo> GetInfo(VoidRequest request, ServerCallContext context)
        {
            var serviceInfo = _translateService.GetServiceInfo();
            return Task.FromResult(new ServiceInfo
            {
                Info = serviceInfo
            });
        }
    }
}
