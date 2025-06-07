using System;
using System.Collections.Generic;

namespace WarehouseConsoleApp.Domain
{
    /// <summary>
    /// Интерфейс для паллеты, содержащей коробки.
    /// </summary>
    public interface IPallet : IWarehouseItem
    {
        /// <summary>Список коробок на паллете.</summary>
        IReadOnlyCollection<IBox> Boxes { get; }

        /// <summary>
        /// Дата окончания срока годности паллеты (минимум среди коробок).
        /// </summary>
        DateTime ExpirationDate { get; }

        /// <summary>Общий вес паллеты (с учётом базового веса).</summary>
        double TotalWeight { get; }

        /// <summary>Общий объём паллеты (с учётом базового объёма).</summary>
        double TotalVolume { get; }
    }
}