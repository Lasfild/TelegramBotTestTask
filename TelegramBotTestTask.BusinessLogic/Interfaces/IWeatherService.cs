using System.Threading.Tasks;

namespace TelegramBotTestTask.BusinessLogic.Interfaces
{
    public interface IWeatherService
    {
        Task<string> GetCurrentWeatherAsync();
    }
}
