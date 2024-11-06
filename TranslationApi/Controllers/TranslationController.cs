using Microsoft.AspNetCore.Mvc;
using TranslationService;

namespace TranslationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslationController : ControllerBase
    {
        private readonly ITranslateService _translateService;

        public TranslationController(ITranslateService translateService)
        {
            _translateService = translateService;
        }

        /// <summary>
        /// Перевести текст с одного языка на другой.
        /// </summary>
        /// <param name="text">Текст для перевода.</param>
        /// <param name="sourceLanguage">Код языка источника.</param>
        /// <param name="targetLanguage">Код языка назначения.</param>
        /// <returns>Переведенный текст.</returns>
        [HttpGet("translate")]
        public async Task<IActionResult> Translate(string text, string sourceLanguage, string targetLanguage)
        {
            try
            {
                var translatedText = await _translateService.TranslateAsync(text, sourceLanguage, targetLanguage);
                return Ok(translatedText);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }


        /// <summary>
        /// Получить информацию о сервисе перевода.
        /// </summary>
        /// <returns>Информация о сервисе.</returns>
        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            try
            {
                var res = _translateService.GetServiceInfo();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
