using System.Threading.Tasks;

public interface IWeatherHistoryRepository
{
    Task SaveWeatherRequestAsync(int userId, string city, string weatherData);
}
