using System;

namespace WarehouseConsoleApp.Configuration
{
    /// <summary>
    /// Параметры генерации данных для склада.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Секция конфигурации в appsettings.json.
        /// </summary>
        public const string SectionName = "Warehouse";

        /// <summary>
        /// Количество паллет для генерации.
        /// </summary>
        public int PalletCount { get; set; } = 5;

        /// <summary>
        /// Минимальное число коробок на паллете.
        /// </summary>
        public int MinBoxes { get; set; } = 1;

        /// <summary>
        /// Максимальное число коробок на паллете.
        /// </summary>
        public int MaxBoxes { get; set; } = 5;
    }
}