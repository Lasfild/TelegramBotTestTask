using System.Threading.Tasks;
using TelegramBotTestTask.DTOs;
using TelegramBotTestTask.DTOs.Responses;

namespace TelegramBotTestTask.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserWithHistoryAsync(int userId);
    }
}
