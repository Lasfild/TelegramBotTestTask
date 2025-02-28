using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TelegramBotTestTask.DTOs.Responses;

namespace TelegramBotTestTask.BusinessLogic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ILogger<WeatherService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["OpenWeather:ApiKey"];
            _logger = logger;
        }

        public async Task<WeatherResponseDto?> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            using var client = _httpClientFactory.CreateClient();

            try
            {
                _logger.LogInformation($"Запрос в OpenWeatherMap: {url}");

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Ошибка запроса к OpenWeatherMap: {response.StatusCode}, URL: {url}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Ответ OpenWeatherMap: {json}");

                var weatherResponse = JsonSerializer.Deserialize<WeatherResponseDto>(json);

                if (weatherResponse == null)
                {
                    _logger.LogError("Ошибка парсинга JSON");
                    return null;
                }

                return weatherResponse;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Ошибка запроса: {e.Message}");
                return null;
            }
            catch (JsonException e)
            {
                _logger.LogError($"Ошибка парсинга JSON: {e.Message}");
                return null;
            }
        }
    }
}
