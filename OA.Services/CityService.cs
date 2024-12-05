using Microsoft.EntityFrameworkCore;
using OA.Core.Domain;
using OA.Data;

namespace OA.Services;

public class CityService : ICityService
{
    protected readonly IRepository<City> _cityRepository;
    protected readonly IRepository<Area> _areaRepository;

    public CityService(IRepository<City> cityRepository, 
        IRepository<Area> areaRepository)
    {
        _cityRepository = cityRepository;
        _areaRepository = areaRepository;
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
}
