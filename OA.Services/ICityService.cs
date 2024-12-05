using OA.Core.Domain;

namespace OA.Services;

public interface ICityService
{
    Task<City> GetCityByIdAsync(int cityId);
    
    Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId);   
    
    Task<Area> GetAreaByIdAsync(int areaId);

    Task<IEnumerable<Area>> GetAreasByCityIdAsync(int cityId);
}
