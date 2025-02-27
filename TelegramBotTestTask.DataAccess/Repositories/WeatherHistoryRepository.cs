using Dapper;
using System.Data;

public class WeatherHistoryRepository : IWeatherHistoryRepository
{
    private readonly IDbConnection _dbConnection;

    public WeatherHistoryRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task SaveWeatherRequestAsync(int userId, string city, string weatherData)
    {
        var query = "INSERT INTO WeatherHistory (UserId, City, WeatherData, RequestDate) VALUES (@UserId, @City, @WeatherData, GETDATE())";
        await _dbConnection.ExecuteAsync(query, new { UserId = userId, City = city, WeatherData = weatherData });
    }
}
