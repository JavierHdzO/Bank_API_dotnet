using Microsoft.EntityFrameworkCore;
using bank_api.Data;
using bank_api.Models;
using bank_api.Models.Dtos;
using bank_api.Interfaces;
using bank_api.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:NpgsqlConnection"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankContext>( options => options.UseNpgsql( connectionString ));
builder.Services.AddScoped<IContextService<UserDto, CreateUserDto, UpdateUserDto>, UserService>();
builder.Services.AddScoped<AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
