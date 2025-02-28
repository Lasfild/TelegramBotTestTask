using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TelegramBotTestTask.DTOs.Responses
{
    public class WeatherResponseDto
    {
        [JsonPropertyName("name")]
        public string City { get; set; }

        [JsonPropertyName("main")]
        public MainInfo Main { get; set; }

        [JsonPropertyName("weather")]
        public List<WeatherInfo> Weather { get; set; }

        public double Temperature => Main?.Temp ?? 0;
        public string Description => Weather?.Count > 0 ? Weather[0].Description : "No description available";
    }

    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
    }

    public class WeatherInfo
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
