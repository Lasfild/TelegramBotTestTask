using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Models;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<List<UserDto>> GetAllUsersAsync();
}
