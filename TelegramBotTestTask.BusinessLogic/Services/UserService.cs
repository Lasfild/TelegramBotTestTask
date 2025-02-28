using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.DataAccess.Interfaces;
using TelegramBotTestTask.DTOs;
using TelegramBotTestTask.DTOs.Responses;

namespace TelegramBotTestTask.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> GetUserWithHistoryAsync(int userId)
        {
            return await _userRepository.GetUserWithHistoryAsync(userId);
        }
    }
}
