using System.Threading.Tasks;

namespace TelegramBotTestTask.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        Task SendWeatherToAllAsync(string city);
    }
}
