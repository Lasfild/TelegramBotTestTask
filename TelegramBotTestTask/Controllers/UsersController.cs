using Microsoft.AspNetCore.Mvc;
using TelegramBotTestTask.BusinessLogic.DTOs;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IWeatherService _weatherService;
    private readonly ITelegramService _telegramService;

    public UsersController(IUserService userService, IWeatherService weatherService, ITelegramService telegramService)
    {
        _userService = userService;
        _weatherService = weatherService;
        _telegramService = telegramService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost("sendWeatherToAll")]
    public async Task<IActionResult> SendWeatherToUsers([FromBody] SendWeatherRequest request)
    {
        if (request == null)
            return BadRequest("Некорректные данные");

        var users = new List<UserDto>();

        if (request.UserId.HasValue)
        {
            var user = await _userService.GetUserByIdAsync(request.UserId.Value);
            if (user != null)
                users.Add(user);
        }
        else
        {
            users = await _userService.GetAllUsersAsync();
        }

        if (users == null || !users.Any())
            return NotFound("Пользователи не найдены");

        var weatherInfo = await _weatherService.GetCurrentWeatherAsync();

        foreach (var user in users)
        {
            await _telegramService.SendMessageAsync(user.TelegramId, $"Погода: {weatherInfo}");
        }

        return Ok($"Сообщение отправлено {users.Count} пользователям.");
    }
}
