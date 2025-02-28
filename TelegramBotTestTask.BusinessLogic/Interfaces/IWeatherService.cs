using TelegramBotTestTask.DTOs.Responses;
using System.Threading.Tasks;

public interface IWeatherService
{
    Task<WeatherResponseDto?> GetWeatherAsync(string city);
}
    