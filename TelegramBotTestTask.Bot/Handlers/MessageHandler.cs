using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.DataAccess.Interfaces;
using TelegramBotTestTask.DTOs.Responses;

namespace TelegramBotTestTask.Bot.Handlers
{
    public class MessageHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IWeatherService _weatherService;
        private readonly IUserRepository _userRepository;

        public MessageHandler(ITelegramBotClient botClient, IWeatherService weatherService, IUserRepository userRepository)
        {
            _botClient = botClient;
            _weatherService = weatherService;
            _userRepository = userRepository;
        }

        public async Task HandleMessageAsync(Message message)
        {
            if (message?.Text != null)
            {
                if (message.Text.StartsWith("/weather"))
                {
                    var city = message.Text.Split(' ').Skip(1).FirstOrDefault();
                    if (string.IsNullOrEmpty(city))
                    {
                        await SendWeatherButtonsAsync(message.Chat.Id);
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

                var user = await _userRepository.GetUserByTelegramIdAsync(message.From.Id);
                if (user == null)
                {
                    var newUser = new UserDto
                    {
                        TelegramId = message.From.Id,
                        Username = message.From.Username
                    };

                    await _userRepository.AddUserAsync(newUser);
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Вы успешно зарегистрированы в системе.");
                }
            }
            else
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, "Сообщение не содержит текста.");
            }
        }

        public async Task SendWeatherButtonsAsync(long chatId)
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Харьков", "/weather Харьков"),
                    InlineKeyboardButton.WithCallbackData("Киев", "/weather Киев"),
                    InlineKeyboardButton.WithCallbackData("Львов", "/weather Львов")
                }
            });

            await _botClient.SendTextMessageAsync(chatId, "Выберите город для получения погоды:", replyMarkup: keyboard);
        }
    }
}
