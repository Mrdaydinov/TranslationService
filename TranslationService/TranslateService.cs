using DeepL;
using System.Collections.Concurrent;

namespace TranslationService
{
    public class TranslateService : ITranslateService
    {
        private readonly string _authKey;
        private readonly Translator _translator;

        private readonly ConcurrentDictionary<string, string> translationCache = new ConcurrentDictionary<string, string>();

        public TranslateService()
        {
            _authKey = "2cac10f6-b1d6-49eb-ae3f-509db8833541:fx";
            _translator = new Translator(_authKey);
        }

        public async Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage)
        {
            string cacheKey = $"{sourceLanguage}:{targetLanguage}:{text}";

            if (translationCache.TryGetValue(cacheKey, out string cachedTranslation))
            {
                return cachedTranslation;
            }
            try
            {
                var translatedText = await _translator.TranslateTextAsync(text, sourceLanguage, targetLanguage);
                translationCache[cacheKey] = translatedText.ToString();
                return translatedText.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Translation error: {ex.Message}");
            }
        }


        public string GetServiceInfo()
        {
            string cacheType = translationCache is ConcurrentDictionary<string, string> ? "In-memory" : "Other";
            var usage = _translator.GetUsageAsync().Result;
            int cacheSize = translationCache.Count;

            return $"Translation Service using DeepL API\n" +
               $"Cache Type: {cacheType}\n" +
               $"Cache Size: {cacheSize} entries\n" +
               $"{usage}";
        }
    }
}