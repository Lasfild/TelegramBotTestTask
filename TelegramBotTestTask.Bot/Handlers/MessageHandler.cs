using Telegram.Bot;
using Telegram.Bot.Types;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBotTestTask.Bot.Handlers
{
    public class MessageHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IWeatherService _weatherService;

        public MessageHandler(ITelegramBotClient botClient, IWeatherService weatherService)
        {
            _botClient = botClient;
            _weatherService = weatherService;
        }

        public async Task HandleMessageAsync(Message message)
        {
            if (message?.Text != null)  // Проверка на null
            {
                if (message.Text.StartsWith("/weather"))
                {
                    var city = message.Text.Split(' ').Skip(1).FirstOrDefault();
                    if (string.IsNullOrEmpty(city))
                    {
                        await _botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, укажите город после команды /weather");
                        return;
                    }

                    var weather = await _weatherService.GetWeatherAsync(city);
                    if (weather == null)
                    {
                        await _botClient.SendTextMessageAsync(message.Chat.Id, "Не удалось получить данные о погоде.");
                        return;
                    }

                    await _botClient.SendTextMessageAsync(message.Chat.Id,
                        $"Погода в {city}: {weather.Temperature}°C, {weather.Description}");
                }
                else
                {
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Используйте команду /weather {город}");
                }
            }
            else
            {
                // Если сообщение не содержит текст, уведомляем пользователя.
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Сообщение не содержит текста.");
            }
        }
    }
}
