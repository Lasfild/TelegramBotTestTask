using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Models;
using TelegramBotTestTask.DataAccess.Models;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        return user == null ? null : new UserDto(user.Id, user.TelegramId, user.Name);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto(u.Id, u.TelegramId, u.Name)).ToList();
    }
}
