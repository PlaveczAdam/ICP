using InfiniteCreativity.Data;
using InfiniteCreativity.Mappers;
using InfiniteCreativity.Models;
using InfiniteCreativity.Services;
using InfiniteCreativity.Services.ItemGeneratorNS;
using InfiniteCreativity.Services.QuestGeneratorNS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Numerics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = String.Empty;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration["ConnectionStrings:InfiniteCreativityConnection"];
}
else
{
    string connectionVariableName = "ASPNETCORE_DB_CONNECTION_STRING";
    connectionString = Environment.GetEnvironmentVariable(connectionVariableName);
    if (connectionString == null)
    {
        Console.WriteLine("No environmental variable name provided: " + connectionVariableName);
        Environment.Exit(-1);
    }
}

builder.Services.AddDbContext<InfiniteCreativityContext>(
    options => options.UseNpgsql(connectionString)
);

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IQuestService, QuestService>();

builder.Services.AddScoped<QuestGenerator>();
builder.Services.AddScoped<ItemGenerator>();

builder.Services
    .AddAuthentication(options => options.DefaultScheme = "Cookies")
    .AddCookie(options =>
    {
        options.Cookie.Name = "ICA";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/api/player/login";
        //todo maybe �tirni
        options.AccessDeniedPath = "/api/player/login";
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<PasswordHasher<Player>>();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
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
