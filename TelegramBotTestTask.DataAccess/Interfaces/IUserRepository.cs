using TelegramBotTestTask.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramBotTestTask.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
