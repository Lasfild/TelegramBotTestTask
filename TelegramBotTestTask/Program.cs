using System.Data;
using Microsoft.Data.SqlClient;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTestTask.Bot.Handlers;
using TelegramBotTestTask.BusinessLogic.Services;
using TelegramBotTestTask.DataAccess.Interfaces;
using TelegramBotTestTask.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Настройка подключения к базе данных
var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));

// Регистрируем остальные сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Регистрируем сервисы бизнес-логики
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Добавляем репозиторий пользователя

// Регистрируем TelegramBotClient
var botToken = configuration["TelegramBot:Token"];
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));

// Регистрируем обработчики
builder.Services.AddScoped<MessageHandler>();
builder.Services.AddScoped<CallbackHandler>();

// Логирование
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();

// Настройка webhook для бота
var webhookUrl = configuration["WebhookUrl"] + "/webhook";
var botClient = app.Services.GetRequiredService<ITelegramBotClient>();
try
{
    await botClient.SetWebhookAsync(webhookUrl);
    app.Logger.LogInformation("Webhook set successfully.");
}
catch (Exception ex)
{
    app.Logger.LogError($"Error setting webhook: {ex.Message}");
}

// Обработка входящих запросов
app.MapPost("/webhook", async (Update update, IServiceProvider services, ILogger<Program> logger) =>
{
    logger.LogInformation($"Received update: {update}");

    try
    {
        var messageHandler = services.GetRequiredService<MessageHandler>();
        var callbackHandler = services.GetRequiredService<CallbackHandler>();

        if (update.Message != null)
        {
            logger.LogInformation($"Message received: {update.Message.Text}");
            await messageHandler.HandleMessageAsync(update.Message);
        }

        if (update.CallbackQuery != null)
        {
            logger.LogInformation($"Callback received: {update.CallbackQuery.Data}");
            await callbackHandler.HandleCallbackAsync(update.CallbackQuery);
        }

        return Results.Ok();
    }
    catch (Exception ex)
    {
        logger.LogError($"Error processing update: {ex.Message}");
        return Results.BadRequest($"Error processing update: {ex.Message}");
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
