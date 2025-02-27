using Microsoft.AspNetCore.Mvc;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost("sendWeatherToAll")]
    public async Task<IActionResult> SendWeatherToAll([FromBody] SendWeatherRequest request)
    {
        await _userService.SendWeatherToAllAsync(request);
        return Ok();
    }
}
