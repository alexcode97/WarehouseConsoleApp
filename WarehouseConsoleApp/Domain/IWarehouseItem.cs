using System;

namespace WarehouseConsoleApp.Domain
{
    /// <summary>
    /// Базовый интерфейс для всех предметов на складе.
    /// </summary>
    public interface IWarehouseItem
    {
        /// <summary>Уникальный идентификатор предмета.</summary>
        Guid Id { get; }

        /// <summary>Ширина, мм.</summary>
        double Width { get; }

        /// <summary>Высота, мм.</summary>
        double Height { get; }

        /// <summary>Глубина, мм.</summary>
        double Depth { get; }

        /// <summary>Вес, кг.</summary>
        double Weight { get; }

        /// <summary>Вычисляемый объём (Width × Height × Depth).</summary>
        double Volume { get; }
    }
}