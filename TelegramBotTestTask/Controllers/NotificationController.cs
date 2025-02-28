using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TelegramBotTestTask.BusinessLogic.Interfaces;

namespace TelegramBotTestTask.Controllers
{
    [ApiController]
    [Route("notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("sendWeatherToAll")]
        public async Task<IActionResult> SendWeatherToAll([FromQuery] string city)
        {
            await _notificationService.SendWeatherToAllAsync(city);
            return Ok("Рассылка завершена.");
        }
    }
}
