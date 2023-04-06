using bank_api.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using bank_api.Data;
using bank_api.Models.Dtos;
using bank_api.Interfaces;
using bank_api.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:NpgsqlConnection"];
var jwtConfiguration = builder.Configuration["Jwt:Key"];

// Add services to the container.

builder.Services.AddControllers( options => {
    options.InputFormatters.Insert(0, JIF.GetJsonPatchInputFormatter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankContext>( options => options.UseNpgsql( connectionString ));
builder.Services.AddTransient(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
builder.Services.AddScoped<IContextService<UserDto, CreateUserDto, UpdateUserDto>, UserService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer( option => {
    option.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(jwtConfiguration!) ),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization( options => {
    options.AddPolicy("AdminRole", policy => policy.RequireClaim("RoleType", "2"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
