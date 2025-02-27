using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace TelegramBotTestTask.BusinessLogic.Interfaces
{
    public interface ITelegramService
    {
        Task HandleUpdateAsync(Update update);
        Task SendMessageAsync(long chatId, string message);
    }
}

