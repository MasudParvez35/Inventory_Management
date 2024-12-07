using OA.Core.Domain;

namespace OA.Services;

public interface ICityService
{
    Task<City> GetCityByIdAsync(int cityId);
    
    Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId);
    Task InsertCityAsync(City city);
    Task UpdateCityAsync(City city);
    Task DeleteCityAsync(City city);
    Task<IEnumerable<City>> GetAllCitiesAsync();
}
