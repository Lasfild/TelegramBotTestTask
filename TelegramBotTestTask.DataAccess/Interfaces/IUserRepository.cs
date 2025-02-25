using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBotTestTask.DataAccess.Models;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int userId);
    Task<IEnumerable<User>> GetAllAsync();
}
