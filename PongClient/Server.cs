using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PongServer;

namespace PongClient
{
    internal class Server
    {
        private Task _serverTask;
        private IHost _host;

        public void StartServer()
        {
            _serverTask = Task.Run(() =>
            {
                var builder = WebApplication.CreateBuilder();
                builder.Services.AddSignalR();
                builder.Services.AddSingleton<PongService>();

                var app = builder.Build();
                app.MapHub<PongHub>("/pong");

                app.Run();
            });
        }

        public async Task StopServerAsync()
        {
            if (_serverTask != null && !_serverTask.IsCompleted)
            {
                _host?.Dispose();
                await _serverTask;
            }
        }
    }
}
