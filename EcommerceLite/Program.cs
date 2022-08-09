using FluentValidation.AspNetCore;
using EcommerceLite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EcommerceLite.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());


builder.Services.AddDbContext<EcommerceLiteDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceLite"));
});

builder.Services.AddDbContext<EcommerceLiteDbContext>();


var region = builder.Configuration.GetConnectionString("EcommerceLite");


builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//builder.Services.AddScoped<IWalkRepository, WalkRepository>();

//builder.Services.AddScoped<IWalkDifficultyRepository, WalkDifficultyRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITokenHandlerRepository, TokenHandlerRepository>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options => options.TokenValidationParameters
       = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(
               Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
       });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();