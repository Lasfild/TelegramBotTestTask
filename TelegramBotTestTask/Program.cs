using Telegram.Bot;
using System.Data;
using Microsoft.Data.SqlClient;
using TelegramBotTestTask.Bot.BotServices;
using TelegramBotTestTask.Bot.Handlers;
using TelegramBotTestTask.BusinessLogic.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();


builder.Services.AddTransient<IDbConnection>(sp =>
    new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWeatherHistoryRepository, WeatherHistoryRepository>();

builder.Services.AddScoped<MessageHandler>();
builder.Services.AddScoped<CallbackHandler>();

var botToken = configuration["TelegramBot:Token"];
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));

builder.Services.AddScoped<TelegramBotService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
