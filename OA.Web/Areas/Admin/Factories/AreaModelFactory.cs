using OA.Core.Domain;
using OA.Services;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories;

public class AreaModelFactory : IAreaModelFactory
{
    #region Fields

    private readonly IStateService _stateservice;
    private readonly ICityService _cityService;
    private readonly IAreaService _areaService;

    #endregion

    #region Ctor

    public AreaModelFactory(IStateService stateservice, 
        ICityService cityService, 
        IAreaService areaService)
    {
        _stateservice = stateservice;
        _cityService = cityService;
        _areaService = areaService;
    }

    #endregion

    #region Methods
    
    public async Task<StateModel> PrepareStateModelAsync(StateModel model, State state)
    {
        if (state == null)
            return model;

        var numberOfCities = (await _cityService.GetCitiesByStateIdAsync(state.Id)).Count();

        model = new StateModel
        {
            Name = state.Name,
            NumberOfCity = numberOfCities,
            DisplayOrder = state.DisplayOrder
        };

        return model;
    }

    public async Task<CityModel> PrepareCityModelAsync(CityModel model, City city)
    {
        if (city == null)
            return model;

        var numberofAreas = (await _areaService.GetAreasByCityIdAsync(city.Id)).Count();

        model = new CityModel
        {
            Name = city.Name,
            NumberOfAreas = numberofAreas,
            DisplayOrder = city.DisplayOrder
        };

        return model;
    }

    public async Task<AreaModel> PrepareAreaModelAsync(AreaModel model, Area area)
    {
        if (area == null)
            return model;

        model = new AreaModel
        {
            Name = area.Name,
            DisplayOrder = area.DisplayOrder
        };

        return model;
    }

    public async Task<StateListModel> PrepareStateListModelAsync()
    {
        var model = new StateListModel();

        var states = await _stateservice.GetAllStatesAsync();
        foreach(var state in states)
            model.States.Add(await PrepareStateModelAsync(null, state));

        return model;
    }

    public async Task<CityListModel> PrepareCityListModelAsync()
    {
        var model = new CityListModel();

        var cities = await _cityService.GetAllCitiesAsync();
        foreach (var city in cities)
            model.Cities.Add(await PrepareCityModelAsync(null, city));

        return model;
    }

    public async Task<AreaListModel> PrepareAreaListModelAsync()
    {
        var model = new AreaListModel();
        
        var areas = await _areaService.GetAllAreasAsync();
        foreach (var area in areas)
            model.Areas.Add(await PrepareAreaModelAsync(null, area));

        return model;
    }

    #endregion
}
