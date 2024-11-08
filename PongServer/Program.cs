using PongServer;
using System.Timers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<PongService>();

var app = builder.Build();

app.MapHub<PongHub>("/pong");

app.Run();