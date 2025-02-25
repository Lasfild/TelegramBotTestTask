using System.Net.Http;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;

namespace TelegramBotTestTask.BusinessLogic.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrentWeatherAsync()
        {
            var response = await _httpClient.GetStringAsync("https://api.open-meteo.com/v1/forecast?latitude=50.45&longitude=30.52&current_weather=true");
            return $"Температура в Киеве: {response}";
        }
    }
}
