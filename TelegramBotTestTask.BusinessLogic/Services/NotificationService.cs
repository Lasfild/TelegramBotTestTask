using Telegram.Bot;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.DataAccess.Interfaces;

namespace TelegramBotTestTask.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IUserRepository _userRepository;
        private readonly IWeatherService _weatherService;

        public NotificationService(ITelegramBotClient botClient, IUserRepository userRepository, IWeatherService weatherService)
        {
            _botClient = botClient;
            _userRepository = userRepository;
            _weatherService = weatherService;
        }

        public async Task SendWeatherToAllAsync(string city)
        {
            var users = await _userRepository.GetAllUserIdsAsync();
            var weather = await _weatherService.GetWeatherAsync(city);

            if (weather == null) return;

            foreach (var userId in users)
            {
                await _botClient.SendTextMessageAsync(userId, $"Погода в {city}: {weather.Temperature}°C, {weather.Description}");
            }
        }
    }
}
