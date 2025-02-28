using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.DTOs;

namespace TelegramBotTestTask.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWithWeatherHistory(int userId)
        {
            var user = await _userService.GetUserWithHistoryAsync(userId);
            if (user == null)
            {
                return NotFound("Пользователь не найден.");
            }
            return Ok(user);
        }
    }
}
