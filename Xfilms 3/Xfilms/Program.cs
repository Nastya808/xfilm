using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Используем builder.Configuration для получения строки подключения
builder.Services.AddDbContext<XFilmContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))); // Изменено на SqliteConnection

// Добавляем сервисы
builder.Services.XfilmsServise();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрация IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache(); // Кэш для хранения сессий
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Время жизни сессии
    options.Cookie.HttpOnly = true; // Установка флага HttpOnly для куки
    options.Cookie.IsEssential = true; // Обязательный для работы приложения
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession(); // Добавьте это

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
