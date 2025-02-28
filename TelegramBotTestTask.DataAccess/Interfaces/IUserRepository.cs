using System.Threading.Tasks;
using TelegramBotTestTask.DTOs.Responses;

namespace TelegramBotTestTask.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserWithHistoryAsync(int userId);
        Task<UserDto> GetUserByTelegramIdAsync(long telegramId);
        Task AddUserAsync(UserDto user);
        Task<IEnumerable<long>> GetAllUserIdsAsync();
    }
}
