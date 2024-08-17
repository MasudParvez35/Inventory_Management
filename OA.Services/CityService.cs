using OA.Core.Domain;
using OA.Data;

namespace OA.Services
{
    public class CityService : ICityService
    {
        protected readonly IRepository<City> _cityRepository;

        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId)
        {
            return await _cityRepository.FindByAsync(x => x.StateId == stateId);
        }

        public async Task<City> GetCityByIdAsync(int cityId)
        {
            return await _cityRepository.GetByIdAsync(cityId);
        }
    }
}
