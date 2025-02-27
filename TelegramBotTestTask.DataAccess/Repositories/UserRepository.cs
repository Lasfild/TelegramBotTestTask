using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TelegramBotTestTask.DTOs;
using TelegramBotTestTask.DataAccess.Interfaces;

namespace TelegramBotTestTask.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserDto>(query, new { Id = id });
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var query = "SELECT * FROM Users";
            return await _dbConnection.QueryAsync<UserDto>(query);
        }
    }
}
