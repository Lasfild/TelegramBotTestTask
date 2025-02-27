using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("sendWeatherToAll")]
    public async Task<IActionResult> SendWeatherToAll()
    {
        await _notificationService.SendWeatherToAllUsersAsync();
        return Ok("Уведомления отправлены");
    }
}
