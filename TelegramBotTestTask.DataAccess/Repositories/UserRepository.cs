using Dapper;
using System.Data;
using TelegramBotTestTask.DTOs.Responses;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _dbConnection.QueryAsync<UserDto>("SELECT * FROM Users");
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<UserDto>(
            "SELECT * FROM Users WHERE Id = @Id", new { Id = userId });
    }
}
