using System;

namespace WarehouseConsoleApp.Domain
{
    /// <summary>
    /// Интерфейс для коробки со сроком годности.
    /// </summary>
    public interface IBox : IWarehouseItem
    {
        /// <summary>Дата окончания срока годности.</summary>
        DateTime ExpirationDate { get; }
    }
}