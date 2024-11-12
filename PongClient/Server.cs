using PongServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace PongClient
{
    internal class Server
    {
        private Task _serverTask;
        private WebApplication app;

        public void StartServer()
        {
            _serverTask = Task.Run(() =>
            {
                var builder = WebApplication.CreateBuilder();
                builder.WebHost.UseUrls("http://0.0.0.0:5000");
                builder.Services.AddSignalR();
                builder.Services.AddSingleton<PongService>();

                app = builder.Build();
                app.MapHub<PongHub>("/pong");
                app.Run();
            });
        }

        public async Task StopServerAsync()
        {
            //if (_serverTask != null && !_serverTask.IsCompleted)
            //{
            //    _host?.Dispose();
            //    await _serverTask;
            //}

            if (app != null)
            {
                //await _host.StopAsync();
                await app.DisposeAsync();
                app = null;
                _serverTask = null;
            }
        }
    }
}
