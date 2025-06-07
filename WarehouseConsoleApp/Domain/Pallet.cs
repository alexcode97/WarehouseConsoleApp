using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseConsoleApp.Domain
{
    /// <summary>
    /// Реализация паллеты с боковой логикой.
    /// </summary>
    public sealed class Pallet : IPallet
    {
        private const double BaseWeight = 30.0;
        private readonly List<IBox> _boxes = new();

        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();
        public double Width { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Weight => BaseWeight;
        public double Volume => Width * Height * Depth;
        public IReadOnlyCollection<IBox> Boxes => _boxes.AsReadOnly();

        /// <inheritdoc />
        public DateTime ExpirationDate => _boxes.Min(b => b.ExpirationDate);

        /// <inheritdoc />
        public double TotalWeight => BaseWeight + _boxes.Sum(b => b.Weight);

        /// <inheritdoc />
        public double TotalVolume => Volume + _boxes.Sum(b => b.Volume);

        /// <summary>
        /// Инициализирует новую паллету с заданными размерами.
        /// </summary>
        public Pallet(double width, double height, double depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
        }

        /// <summary>
        /// Добавляет коробку на паллету, проверяя размеры.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Когда ширина или глубина коробки превышает параметры паллеты.
        /// </exception>
        public void AddBox(IBox box)
        {
            if (box.Width > Width || box.Depth > Depth)
                throw new ArgumentOutOfRangeException(nameof(box), "Box exceeds pallet dimensions.");

            _boxes.Add(box);
        }
    }
}