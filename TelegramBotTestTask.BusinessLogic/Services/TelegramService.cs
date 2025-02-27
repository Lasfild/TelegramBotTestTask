using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;

namespace TelegramBotTestTask.BusinessLogic.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient _botClient;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        // Конструктор для внедрения IConfiguration для доступа к настройкам
        public TelegramService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            var token = _configuration["TelegramBot:Token"];  // Токен для Telegram
            _botClient = new TelegramBotClient(token);  // Инициализация клиента с токеном
        }

        public async Task HandleUpdateAsync(Update update)
        {
            if (update.Message?.Text != null)
            {
                var city = update.Message.Text.Split(" ")[1];
                var weather = await GetWeatherForCity(city);  // Получение погоды
                await _botClient.SendTextMessageAsync(update.Message.Chat.Id, weather);  // Отправка сообщения
            }
        }

        // Отправка сообщения пользователю
        public async Task SendMessageAsync(long chatId, string message)
        {
            await _botClient.SendTextMessageAsync(chatId, message);
        }

        // Получение погоды через OpenWeather API
        private async Task<string> GetWeatherForCity(string city)
        {
            var apiKey = _configuration["OpenWeather:ApiKey"];  // Токен для OpenWeather
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=ru";

            var response = await _httpClient.GetStringAsync(url);
            dynamic weatherData = JsonConvert.DeserializeObject(response);

            if (weatherData != null && weatherData.main != null)
            {
                var temperature = weatherData.main.temp;
                var weatherDescription = weatherData.weather[0].description;
                return $"Погода в {city}: {weatherDescription}, {temperature}°C";
            }

            return "Не удалось получить данные о погоде.";
        }
    }
}
