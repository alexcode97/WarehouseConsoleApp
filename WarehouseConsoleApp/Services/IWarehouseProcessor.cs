using System.Threading;
using System.Threading.Tasks;

namespace WarehouseConsoleApp.Services
{
    /// <summary>
    /// Сервис генерирует данные и выводит результаты в консоль.
    /// </summary>
    public interface IWarehouseProcessor
    {
        /// <summary>
        /// Запуск обработки склада.
        /// </summary>
        Task RunAsync(CancellationToken cancellationToken);
    }
}