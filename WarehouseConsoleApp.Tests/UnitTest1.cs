using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WarehouseConsoleApp.Services;
using Xunit;

namespace WarehouseConsoleApp.Tests
{
    /// <summary>
    /// Юнит-тесты для <see cref="WarehouseProcessor" />.
    /// </summary>
    public class WarehouseProcessorTests
    {
        /// <summary>
        /// Проверяет, что метод <see cref="WarehouseProcessor.RunAsync"/> успешно выполняется с корректными настройками.
        /// </summary>
        [Fact]
        public async Task RunAsync_DefaultSettings_DoesNotThrow()
        {
            // Arrange: настройка параметров секции Warehouse в памяти
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Warehouse:PalletCount", "2" },
                { "Warehouse:MinBoxes", "1" },
                { "Warehouse:MaxBoxes", "2" }
            };
            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Используем NullLogger, чтобы игнорировать вывод логов
            ILogger<WarehouseProcessor> logger = NullLogger<WarehouseProcessor>.Instance;

            var processor = new WarehouseProcessor(logger, config);

            // Act & Assert: метод не должен выкидывать исключений
            await processor.RunAsync(CancellationToken.None);
        }

        /// <summary>
        /// Проверяет, что конструктор выбрасывает <see cref="System.InvalidOperationException"/> при отсутствии секции конфигурации.
        /// </summary>
        [Fact]
        public void Constructor_MissingConfig_ThrowsInvalidOperationException()
        {
            // Arrange: пустая конфигурация без секции Warehouse
            IConfiguration config = new ConfigurationBuilder().Build();
            ILogger<WarehouseProcessor> logger = NullLogger<WarehouseProcessor>.Instance;

            // Act & Assert: конструктор должен выбросить исключение
            Assert.Throws<System.InvalidOperationException>(() => new WarehouseProcessor(logger, config));
        }
    }
}
