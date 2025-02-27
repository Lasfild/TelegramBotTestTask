using System.Threading.Tasks;

public interface INotificationService
{
    Task SendWeatherToAllUsersAsync();
}
