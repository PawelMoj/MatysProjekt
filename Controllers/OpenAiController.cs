using MatysProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatysProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class OpenAiController : ControllerBase
    {
        private readonly IOpenAiService _openAiService;

        public OpenAiController(IOpenAiService openAiService)
        {
            _openAiService = openAiService;
        }

        [HttpGet]
        public async Task<IActionResult> CompleteSentence(string text)
        {
            var result = await _openAiService.CopleteSentence(text);
            return Ok(result);
        }
    }
}
