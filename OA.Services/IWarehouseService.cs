using OA.Core.Domain;

namespace OA.Services;

public interface IWarehouseService
{
    Task DeleteWarehouseAsync(Warehouse warehouse);
    Task<Warehouse> GetWarehouseByIdAsync(int warehouseId);
    Task<IEnumerable<Warehouse>> GetWarehousesAsync();
    Task InsertWarehouseAsync(Warehouse warehouse);
    Task UpdateWarehouseAsync(Warehouse warehouse);
}
