using Microsoft.EntityFrameworkCore;
using OA.Core.Domain;
using OA.Data;

namespace OA.Services;

public class AreaService : IAreaService
{
    private readonly IRepository<Area> _areaRepository;

    public AreaService(IRepository<Area> areaRepository)
    {
        _areaRepository = areaRepository;
    }

    #region Methods

    public async Task<Area> GetAreaByIdAsync(int areaId)
    {
        return await _areaRepository.GetByIdAsync(areaId);
    }

    public async Task<IEnumerable<Area>> GetAreasByCityIdAsync(int cityId)
    {
        return await _areaRepository.Table
            .Where(x => x.CityId == cityId)
            .ToListAsync();
    }

    public async Task InsertAreaAsync(Area area)
    {
        await _areaRepository.InsertAsync(area);
    }

    public async Task UpdateAreaAsync(Area area)
    {
        await _areaRepository.UpdateAsync(area);
    }

    public async Task DeleteAreaAsync(Area area)
    {
        await _areaRepository.DeleteAsync(area);
    }

    public async Task<IEnumerable<Area>> GetAllAreasAsync()
    {
        return await _areaRepository.GetAllAsync();
    }

    #endregion
}
