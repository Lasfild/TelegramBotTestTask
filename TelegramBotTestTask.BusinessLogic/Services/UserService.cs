using TelegramBotTestTask.DTOs;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.DataAccess.Interfaces;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITelegramService _telegramService;
    private readonly IWeatherService _weatherService;

    public UserService(IUserRepository userRepository, ITelegramService telegramService, IWeatherService weatherService)
    {
        _userRepository = userRepository;
        _telegramService = telegramService;
        _weatherService = weatherService;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task SendWeatherToAllAsync(SendWeatherRequest request)
    {
        var users = await _userRepository.GetAllUsersAsync();

        foreach (var user in users)
        {
            var weather = await _weatherService.GetWeatherAsync("Moscow"); // Здесь можно передавать город
            await _telegramService.SendMessageAsync(user.TelegramId, weather);
        }
    }
}
