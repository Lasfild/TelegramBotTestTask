using Telegram.Bot.Types;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TelegramBotTestTask.Controllers
{
    [Route("api/telegram")]
    [ApiController]
    public class TelegramBotController : ControllerBase
    {
        private readonly ITelegramService _telegramService;

        public TelegramBotController(ITelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update == null)
                return BadRequest("Update is null");

            await _telegramService.HandleUpdateAsync(update);
            return Ok();
        }
    }
}
