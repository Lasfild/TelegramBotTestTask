using System.Text.Json;
using TelegramBotTestTask.DTOs.Responses;
using Microsoft.Extensions.Configuration;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _apiKey = configuration["OpenWeatherMap:ApiKey"];
    }

    public async Task<WeatherResponseDto?> GetWeatherAsync(string city)
    {
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
        var response = await _httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<WeatherResponseDto>(response);
    }
}
