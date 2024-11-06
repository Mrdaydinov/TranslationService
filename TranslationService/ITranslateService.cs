namespace TranslationService
{
    public interface ITranslateService
    {
        Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage);
        string GetServiceInfo();
    }
}
