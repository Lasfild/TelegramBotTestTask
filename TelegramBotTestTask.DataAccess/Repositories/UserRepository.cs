using System.Data;
using System.Threading.Tasks;
using Dapper;
using TelegramBotTestTask.DataAccess.Interfaces;
using TelegramBotTestTask.DTOs.Responses;

namespace TelegramBotTestTask.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserDto> GetUserWithHistoryAsync(int userId)
        {
            var query = "SELECT UserId, Username, TelegramId, WeatherQueryHistory FROM Users WHERE UserId = @UserId";
            var user = await _dbConnection.QueryFirstOrDefaultAsync<UserDto>(query, new { UserId = userId });
            return user;
        }

        public async Task<UserDto> GetUserByTelegramIdAsync(long telegramId)
        {
            var query = "SELECT UserId, Username, TelegramId, WeatherQueryHistory FROM Users WHERE TelegramId = @TelegramId";
            var user = await _dbConnection.QueryFirstOrDefaultAsync<UserDto>(query, new { TelegramId = telegramId });
            return user;
        }

        public async Task AddUserAsync(UserDto user)
        {
            var query = "INSERT INTO Users (TelegramId, Username) VALUES (@TelegramId, @Username)";
            await _dbConnection.ExecuteAsync(query, new { user.TelegramId, user.Username });
        }

        public async Task<IEnumerable<long>> GetAllUserIdsAsync()
        {
            string query = "SELECT TelegramId FROM Users";
            return await _dbConnection.QueryAsync<long>(query);
        }
    }
}
