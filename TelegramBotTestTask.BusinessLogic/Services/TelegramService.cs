using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TelegramBotTestTask.BusinessLogic.Interfaces;

namespace TelegramBotTestTask.BusinessLogic.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly HttpClient _httpClient;
        private const string BotToken = "ВАШ_ТОКЕН_БОТА";
        private const string OpenWeatherApiKey = "ВАШ_API_КЛЮЧ"; // Получи на openweathermap.org

        public TelegramService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendMessageAsync(long telegramId, string message)
        {
            var url = $"https://api.telegram.org/bot{BotToken}/sendMessage?chat_id={telegramId}&text={message}";
            await _httpClient.GetAsync(url);
        }

        public async Task HandleMessageAsync(long telegramId, string message)
        {
            if (message.StartsWith("/weather"))
            {
                var city = message.Replace("/weather", "").Trim();
                if (string.IsNullOrEmpty(city))
                {
                    await SendMessageAsync(telegramId, "Введите город после /weather.");
                    return;
                }

                var weatherInfo = await GetWeatherAsync(city);
                await SendMessageAsync(telegramId, weatherInfo);
            }
            else
            {
                await SendMessageAsync(telegramId, "Используйте команду /weather {город} для получения погоды.");
            }
        }

        private async Task<string> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={OpenWeatherApiKey}&units=metric";
            var response = await _httpClient.GetStringAsync(url);
            var weatherData = JObject.Parse(response);

            var temperature = weatherData["main"]?["temp"]?.ToString();
            var description = weatherData["weather"]?[0]?["description"]?.ToString();
            return $"Погода в {city}: {description}, {temperature}°C";
        }
    }
}
