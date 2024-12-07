using OA.Core.Domain;
using OA.Data;

namespace OA.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IRepository<Warehouse> _warehouseRepository;

    public WarehouseService(IRepository<Warehouse> warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
    {
        return await _warehouseRepository.GetAllAsync();
    }

    public async Task<Warehouse> GetWarehouseByIdAsync(int warehouseId)
    {
        return await _warehouseRepository.GetByIdAsync(warehouseId);
    }

    public async Task InsertWarehouseAsync(Warehouse warehouse)
    {
        await _warehouseRepository.InsertAsync(warehouse);
    }

    public async Task UpdateWarehouseAsync(Warehouse warehouse)
    {
        await _warehouseRepository.UpdateAsync(warehouse);
    }

    public async Task DeleteWarehouseAsync(Warehouse warehouse)
    {
        await _warehouseRepository.DeleteAsync(warehouse);
    }
}
