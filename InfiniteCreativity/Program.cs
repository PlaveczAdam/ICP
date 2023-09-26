using InfiniteCreativity.Data;
using InfiniteCreativity.HostedServices;
using InfiniteCreativity.Hubs;
using InfiniteCreativity.Mappers;
using InfiniteCreativity.Middlewares;
using InfiniteCreativity.Models.CoreNS;
using InfiniteCreativity.Services.CoreNS;
using InfiniteCreativity.Services.CoreNS.ItemGeneratorNS;
using InfiniteCreativity.Services.CoreNS.QuestGeneratorNS;
using InfiniteCreativity.Services.GameNS;
using InfiniteCreativity.Services.GameNS.EnemyGeneratorNS;
using InfiniteCreativity.Services.MapPatherNS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson((o) => {
    o.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
    o.SerializerSettings.Converters.Add(new StringEnumConverter
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    });
});

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
    options => { 
        options.UseNpgsql(connectionString);
        options.ConfigureWarnings(warnings =>
            warnings.Ignore(CoreEventId.NavigationBaseIncludeIgnored));
    }
);

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IQuestService, QuestService>();
builder.Services.AddScoped<IListingService, ListingService>();
builder.Services.AddScoped<IMessagesService, MessagesService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IGameService,  GameService>();
builder.Services.AddScoped<IGameEndService, GameEndService>();

builder.Services.AddScoped<EnemyGenerator>();
builder.Services.AddScoped<QuestGenerator>();
builder.Services.AddScoped<ItemGenerator>();
builder.Services.AddHostedService<QuestScheduler>();
builder.Services.AddScoped<MapPather>();
builder.Services.AddScoped<TurnSimulator>();

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
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<PasswordHasher<Player>>();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseHttpsRedirection();*/

/*app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000"));*/

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.MapHub<NotificationHub>("/notification");
app.MapHub<GameNotificationHub>("/gnotification");

app.Run();
