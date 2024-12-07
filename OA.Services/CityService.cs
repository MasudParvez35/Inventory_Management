using Microsoft.EntityFrameworkCore;
using OA.Core.Domain;
using OA.Data;

namespace OA.Services;

public class CityService : ICityService
{
    protected readonly IRepository<City> _cityRepository;

    public CityService(IRepository<City> cityRepository)
    {
        _cityRepository = cityRepository;
    }

    #region Methods

    public async Task InsertCityAsync(City city)
    {
        await _cityRepository.InsertAsync(city);
    }

    public async Task UpdateCityAsync(City city)
    {
        await _cityRepository.UpdateAsync(city);
    }

    public async Task DeleteCityAsync(City city)
    {
        await _cityRepository.DeleteAsync(city);
    }

    public async Task<IEnumerable<City>> GetCitiesByStateIdAsync(int stateId)
    {
        return await _cityRepository.Table
            .Where(x => x.StateId == stateId)
            .ToListAsync();
    }

    public async Task<City> GetCityByIdAsync(int cityId)
    {
        return await _cityRepository.GetByIdAsync(cityId);
    }

    public async Task<IEnumerable<City>> GetAllCitiesAsync()
    {
        return await _cityRepository.GetAllAsync();
    }

    #endregion
}
