﻿using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTestTask.BusinessLogic.Interfaces;

namespace TelegramBotTestTask.Bot.Handlers
{
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
            if (callbackQuery.Data.StartsWith("weather "))
            {
                var city = callbackQuery.Data.Replace("weather ", "");
                var weather = await _weatherService.GetWeatherAsync(city);

                if (weather != null)
                {
                    await _botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                        $"Погода в {city}: {weather.Temperature}°C, {weather.Description}");
                }
                else
                {
                    await _botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                        "Не удалось получить данные о погоде.");
                }
            }
        }
    }
}
