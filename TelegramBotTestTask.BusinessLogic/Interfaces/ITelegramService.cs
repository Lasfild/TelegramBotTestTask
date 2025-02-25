using System.Threading.Tasks;

namespace TelegramBotTestTask.BusinessLogic.Interfaces
{
    public interface ITelegramService
    {
        Task SendMessageAsync(long telegramId, string message);
    }
}
