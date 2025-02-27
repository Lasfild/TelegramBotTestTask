using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using TelegramBotTestTask.BusinessLogic.Interfaces;
using TelegramBotTestTask.BusinessLogic.Services;
using TelegramBotTestTask.DataAccess.Interfaces;
using TelegramBotTestTask.DataAccess.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;  // ��� Swagger

var builder = WebApplication.CreateBuilder(args);

// ���������� ������������
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// ���������� �����������
builder.Services.AddScoped<IUserRepository, UserRepository>();  // ����������� �������������
builder.Services.AddScoped<IUserService, UserService>();        // ������ ������ � ��������������
builder.Services.AddScoped<IWeatherService, WeatherService>();  // ������ ������ � �������
builder.Services.AddScoped<ITelegramService, TelegramService>();  // Telegram ������

// ���������� HTTP-������ ��� �������������� � �������� API (��� OpenWeather)
builder.Services.AddHttpClient();

// ���������� ���� ������ ����� Dapper
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� ������� ��� ������ � �������������
builder.Services.AddControllers();  // ��� ����� ��� ������ � API �������������

// ��������� ����������� (���� ������������ � �������)
builder.Services.AddAuthorization();

// ��������� Swagger
builder.Services.AddSwaggerGen();  // ��� ������ ��������� ��������� Swagger

var app = builder.Build();

// ������������ Swagger � ��������� ��������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // �������� Swagger
    app.UseSwaggerUI(); // �������� UI ��� Swagger
}

// �������� ����������� (���� ����������)
app.UseAuthorization();

// ���������� �����������
app.MapControllers();

app.Run();
