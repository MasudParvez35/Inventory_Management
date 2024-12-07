using OA.Core.Domain;

namespace OA.Services;

public interface IAreaService
{
    Task InsertAreaAsync(Area area);
    
    Task UpdateAreaAsync(Area area);
    
    Task DeleteAreaAsync(Area area);

    Task<Area> GetAreaByIdAsync(int areaId);

    Task<IEnumerable<Area>> GetAreasByCityIdAsync(int cityId);

    Task<IEnumerable<Area>> GetAllAreasAsync();
}
