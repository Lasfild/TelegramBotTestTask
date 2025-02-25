using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TelegramBotTestTask.DataAccess.Models;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _db;

    public UserRepository(IConfiguration config)
    {
        _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _db.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Id = @UserId", new { UserId = userId });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _db.QueryAsync<User>("SELECT * FROM Users");
    }
}
