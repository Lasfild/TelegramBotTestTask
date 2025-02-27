using Telegram.Bot;
using Telegram.Bot.Types;

public class CallbackHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IWeatherService _weatherService;

    public CallbackHandler(ITelegramBotClient botClient, IWeatherService weatherService)
    {
        _botClient = botClient;
        _weatherService = weatherService;
    }

    public async Task HandleCallbackAsync(CallbackQuery callbackQuery)
    {
        if (callbackQuery.Data.StartsWith("weather_"))
        {
            var city = callbackQuery.Data.Split('_').Last();
            var weather = await _weatherService.GetWeatherAsync(city);
            if (weather != null)
            {
                await _botClient.EditMessageTextAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                    $"Погода в {city}: {weather.Temperature}°C, {weather.Description}");
            }
        }
    }
}
