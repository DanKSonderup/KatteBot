using KatteBotDiscordBot.Features.Lobbies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using NetCord.Hosting.Gateway;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDiscordGateway();

var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");

builder.Services.AddSingleton<IMongoClient>(x => new MongoClient(connectionString));
builder.Services.AddSingleton<LobbyService>();

var host = builder.Build();

await host.RunAsync();

