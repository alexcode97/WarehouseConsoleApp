using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WarehouseConsoleApp.Configuration;
using WarehouseConsoleApp.Domain;

namespace WarehouseConsoleApp.Services
{
    /// <inheritdoc />
    public class WarehouseProcessor : IWarehouseProcessor
    {
        private readonly ILogger<WarehouseProcessor> _logger;
        private readonly AppSettings _settings;
        private readonly Random _random = new();

        /// <summary>
        /// Конструктор с DI-параметрами.
        /// </summary>
        public WarehouseProcessor(ILogger<WarehouseProcessor> logger, IConfiguration config)
        {
            _logger = logger;
            _settings = config.GetSection(AppSettings.SectionName).Get<AppSettings>()
                        ?? throw new InvalidOperationException("Missing Warehouse config");
        }

        /// <inheritdoc />
        public Task RunAsync(CancellationToken cancellationToken)
        {
            var pallets = GeneratePallets().ToList();

            DisplayGroupedByExpiration(pallets);
            DisplayTop3ByLatestBoxExpiration(pallets);

            return Task.CompletedTask;
        }

        private IEnumerable<IPallet> GeneratePallets()
        {
            for (int i = 0; i < _settings.PalletCount; i++)
            {
                var pallet = new Pallet(120, 15, 100);
                int count = _random.Next(_settings.MinBoxes, _settings.MaxBoxes + 1);

                for (int j = 0; j < count; j++)
                {
                    double w = _random.Next(20, 50);
                    double h = _random.Next(10, 40);
                    double d = _random.Next(20, 60);
                    double weight = _random.Next(5, 20);

                    IBox box = (_random.NextDouble() < 0.5)
                        ? new Box(w, h, d, weight, DateTime.Today.AddDays(-_random.Next(0, 200)))
                        : new Box(w, h, d, weight, DateTime.Today.AddDays(_random.Next(0, 200)), true);

                    try
                    {
                        pallet.AddBox(box);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        _logger.LogWarning(ex, "Skipped invalid box for pallet {PalletId}", pallet.Id);
                    }
                }

                yield return pallet;
            }
        }

        private void DisplayGroupedByExpiration(IEnumerable<IPallet> pallets)
        {
            var grouped = pallets.GroupBy(p => p.ExpirationDate).OrderBy(g => g.Key);
            _logger.LogInformation("Grouped pallets by expiration (ascending):");
            foreach (var group in grouped)
            {
                Console.WriteLine($"\nExpiration: {group.Key:yyyy-MM-dd}");
                foreach (var p in group.OrderBy(p => p.TotalWeight))
                {
                    Console.WriteLine($"Pallet {p.Id:N} - Weight: {p.TotalWeight:F2}, Volume: {p.TotalVolume:F2}, Boxes: {p.Boxes.Count}");
                }
            }
        }

        private void DisplayTop3ByLatestBoxExpiration(IEnumerable<IPallet> pallets)
        {
            var top3 = pallets.OrderByDescending(p => p.Boxes.Max(b => b.ExpirationDate))
                              .Take(3)
                              .OrderBy(p => p.TotalVolume);

            _logger.LogInformation("\nTop 3 pallets with latest box expiration, sorted by volume:");
            foreach (var p in top3)
            {
                Console.WriteLine($"Pallet {p.Id:N} - Expiration: {p.ExpirationDate:yyyy-MM-dd}, Volume: {p.TotalVolume:F2}, Weight: {p.TotalWeight:F2}");
            }
        }
    }
}