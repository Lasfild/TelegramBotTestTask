using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.BusinessLogic.Services;
using TelegramBotTestTask.DataAccess.Interfaces;
using TelegramBotTestTask.DataAccess.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;  // Для Swagger

var builder = WebApplication.CreateBuilder(args);

// Подключаем конфигурацию
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Подключаем зависимости
builder.Services.AddScoped<IUserRepository, UserRepository>();  // Репозиторий пользователей
builder.Services.AddScoped<IUserService, UserService>();        // Логика работы с пользователями
builder.Services.AddScoped<IWeatherService, WeatherService>();  // Логика работы с погодой
builder.Services.AddScoped<ITelegramService, TelegramService>();  // Telegram сервис

// Подключаем HTTP-клиент для взаимодействия с внешними API (для OpenWeather)
builder.Services.AddHttpClient();

// Подключаем базу данных через Dapper
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавляем сервисы для работы с контроллерами
builder.Services.AddControllers();  // Это нужно для работы с API контроллерами

// Добавляем авторизацию (если используется в проекте)
builder.Services.AddAuthorization();

// Добавляем Swagger
builder.Services.AddSwaggerGen();  // Эта строка добавляет поддержку Swagger

var app = builder.Build();

// Конфигурация Swagger и остальных сервисов
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Включаем Swagger
    app.UseSwaggerUI(); // Включаем UI для Swagger
}

// Включаем авторизацию (если используем)
app.UseAuthorization();

// Используем контроллеры
app.MapControllers();

app.Run();
