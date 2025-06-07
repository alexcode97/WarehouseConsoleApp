using System;

namespace WarehouseConsoleApp.Domain
{
    /// <summary>
    /// Реализация коробки с вычислением срока годности.
    /// </summary>
    public sealed class Box : IBox
    {
        private const int ShelfLifeDays = 100;

        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Weight { get; }
        public DateTime ExpirationDate { get; }
        public double Volume => Width * Height * Depth;

        /// <summary>
        /// Создаёт коробку на основе даты производства.
        /// Срок годности = productionDate + <see cref="ShelfLifeDays"/>.
        /// </summary>
        public Box(double width, double height, double depth, double weight, DateTime productionDate)
        {
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            ExpirationDate = productionDate.Date.AddDays(ShelfLifeDays);
        }

        /// <summary>
        /// Создаёт коробку с явным сроком годности.
        /// </summary>
        public Box(double width, double height, double depth, double weight, DateTime expirationDate, bool isExplicit)
        {
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
            ExpirationDate = expirationDate.Date;
        }
    }
}