using PongServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<PongHub>();

var app = builder.Build();

app.MapHub<PongHub>("/pong");

app.Run();
