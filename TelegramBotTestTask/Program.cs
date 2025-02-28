using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTestTask.Bot.Handlers;
using TelegramBotTestTask.BusinessLogic.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IWeatherService, WeatherService>();

var botToken = configuration["TelegramBot:Token"];
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));

builder.Services.AddScoped<MessageHandler>();
builder.Services.AddScoped<CallbackHandler>();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();

var webhookUrl = configuration["WebhookUrl"] + "/webhook";
var botClient = app.Services.GetRequiredService<ITelegramBotClient>();
try
{
    await botClient.SetWebhookAsync(webhookUrl);
    app.Logger.LogInformation("Вебхук успешно настроен.");
}
catch (Exception ex)
{
    app.Logger.LogError($"Ошибка при настройке вебхука: {ex.Message}");
}

// Обработка входящих обновлений
app.MapPost("/webhook", async (Update update, IServiceProvider services, ILogger<Program> logger) =>
{
    logger.LogInformation($"Получено обновление: {update}");

    try
    {
        var messageHandler = services.GetRequiredService<MessageHandler>();
        var callbackHandler = services.GetRequiredService<CallbackHandler>();

        if (update.Message != null)
        {
            logger.LogInformation($"Получено сообщение: {update.Message.Text}");
            await messageHandler.HandleMessageAsync(update.Message);
        }

        if (update.CallbackQuery != null)
        {
            logger.LogInformation($"Получен callback: {update.CallbackQuery.Data}");
            await callbackHandler.HandleCallbackAsync(update.CallbackQuery);
        }

        return Results.Ok();
    }
    catch (Exception ex)
    {
        logger.LogError($"Ошибка при обработке обновления: {ex.Message}");
        return Results.BadRequest($"Ошибка при обработке обновления: {ex.Message}");
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
