using TelegramBotTestTask.DTOs;
using System.Threading.Tasks;

namespace TelegramBotTestTask.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task SendWeatherToAllAsync(SendWeatherRequest request);
    }
}
