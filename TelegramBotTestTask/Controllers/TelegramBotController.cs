using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramBotTestTask.BusinessLogic.Interfaces;

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
        if (update.Message?.Text != null)
        {
            var chatId = update.Message.Chat.Id;
            var message = update.Message.Text;
            await _telegramService.HandleMessageAsync(chatId, message);
        }

        return Ok();
    }
}
