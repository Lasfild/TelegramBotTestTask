using Telegram.Bot;

public class NotificationService : INotificationService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserRepository _userRepository;
    private readonly IWeatherService _weatherService;

    public NotificationService(
        ITelegramBotClient botClient,
        IUserRepository userRepository,
        IWeatherService weatherService)
    {
        _botClient = botClient;
        _userRepository = userRepository;
        _weatherService = weatherService;
    }

    public async Task SendWeatherToAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();

        foreach (var user in users)
        {
            var weather = await _weatherService.GetWeatherAsync(user.LastCity);
            if (weather != null)
            {
                await _botClient.SendTextMessageAsync(user.TelegramId,
                    $"Погода в {user.LastCity}: {weather.Temperature}°C, {weather.Description}");
            }
        }
    }
}
