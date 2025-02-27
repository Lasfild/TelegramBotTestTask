using Telegram.Bot;
using Microsoft.Extensions.Configuration;

public class TelegramBotClientFactory
{
    public static ITelegramBotClient Create(IConfiguration configuration)
    {
        var token = configuration["TelegramBot:Token"];
        return new TelegramBotClient(token);
    }
}
