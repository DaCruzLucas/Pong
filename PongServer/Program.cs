using PongServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddSingleton<PongService>();

var app = builder.Build();

app.MapHub<PongHub>("/pong");

app.Run();