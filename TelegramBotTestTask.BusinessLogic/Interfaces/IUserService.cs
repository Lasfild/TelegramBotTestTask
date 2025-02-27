using TelegramBotTestTask.DTOs.Responses;
using System.Threading.Tasks;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(int userId);
}
