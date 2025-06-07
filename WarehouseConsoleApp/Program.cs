using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WarehouseConsoleApp.Configuration;
using WarehouseConsoleApp.Services;

namespace WarehouseConsoleApp
{
    /// <summary>
    /// Запускает приложение через Generic Host.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Точка входа.
        /// </summary>
        private static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.Configure<AppSettings>(context.Configuration.GetSection(AppSettings.SectionName));
                    services.AddTransient<IWarehouseProcessor, WarehouseProcessor>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .Build();

            var processor = host.Services.GetRequiredService<IWarehouseProcessor>();
            await processor.RunAsync(CancellationToken.None);
        }
    }
}