using IdGen.DependencyInjection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:6379");
var redis = connectionMultiplexer.GetDatabase();
string key = "appname";
var id = redis.StringIncrement(key);
if (id > 1023)
{
    id = 0;
    redis.StringSet(key, id);
}

builder.Services.AddIdGen((int)id);

var app = builder.Build();
app.Logger.LogInformation("IdGen: generatorId = {0}", id);

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
