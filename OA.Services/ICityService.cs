using OA.Core.Domain;

namespace OA.Services
{
    public interface ICityService
    {
        Task<City> GetCityByIdAsync(int cityId);
        Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId);   
        Task<IEnumerable<City>> GetAllCitiesAsync();
    }
}
