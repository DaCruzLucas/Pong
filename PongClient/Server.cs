using PongServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PongClient
{
    internal class Server
    {
        private Task _serverTask;
        private WebApplication _host;

        public void StartServer()
        {
            _serverTask = Task.Run(() =>
            {
                var builder = WebApplication.CreateBuilder();
                builder.Services.AddSignalR();
                builder.Services.AddSingleton<PongService>();

                _host = builder.Build();
                _host.MapHub<PongHub>("/pong");

                _host.Run();
            });
        }

        public async Task StopServerAsync()
        {
            //if (_serverTask != null && !_serverTask.IsCompleted)
            //{
            //    _host?.Dispose();
            //    await _serverTask;
            //}

            if (_host != null)
            {
                //await _host.StopAsync();
                await _host.DisposeAsync();
                _host = null;
                _serverTask = null;
            }
        }
    }
}
