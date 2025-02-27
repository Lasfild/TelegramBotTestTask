using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;

namespace TelegramBotTestTask.BusinessLogic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenWeather:ApiKey"];  // Чтение API-ключа из конфигурации
        }

        public async Task<string> GetWeatherAsync(string city)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
            if (!response.IsSuccessStatusCode)
                return "Ошибка получения погоды. Проверьте название города.";

            var weatherData = await response.Content.ReadAsStringAsync();
            return $"Погода в {city}: {weatherData}";
        }
    }
}
