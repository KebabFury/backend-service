using System.Reflection;
using KebabFury.Innopolice.WebApi.Application.Services;
using KebabFury.Innopolice.WebApi.Application.Settings;
using KebabFury.Innopolice.WebApi.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAllSettings(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureAllServices();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File(
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/log-.txt"),
        rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
