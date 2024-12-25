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

    #region State

    public async Task<StateModel> PrepareStateModelAsync(StateModel model, State state)
    {
        if (state != null)
        {
            if (model == null)
            {
                var numberOfCities = (await _cityService.GetCitiesByStateIdAsync(state.Id)).Count();

                model = new StateModel
                {
                    Id = state.Id,
                    Name = state.Name,
                    NumberOfCity = numberOfCities,
                    DisplayOrder = state.DisplayOrder
                };
            }

            var cityListModel = await PrepareCityListModelAsync(state);
            model.Cities = cityListModel.Cities;
        }
        
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

    #endregion

    #region City

    public async Task<CityModel> PrepareCityModelAsync(CityModel model, City city)
    {
        if (city != null)
        {
            if (model == null)
            {
                var numberofAreas = (await _areaService.GetAreasByCityIdAsync(city.Id)).Count();

                model = new CityModel()
                {
                    Id = city.Id,
                    Name = city.Name,
                    NumberOfAreas = numberofAreas,
                    DisplayOrder = city.DisplayOrder
                };
            }
        }

        return model;
    }

    public async Task<CityListModel> PrepareCityListModelAsync(State state)
    {
        var model = new CityListModel();

        var cities = await _cityService.GetCitiesByStateIdAsync(state.Id);
        
        foreach (var city in cities)
            model.Cities.Add(await PrepareCityModelAsync(null, city));

        return model;
    }

    #endregion

    #region Area

    public async Task<AreaModel> PrepareAreaModelAsync(AreaModel model, Area area)
    {
        if (area != null)
        {
            if (model == null)
            {
                model = new AreaModel()
                {
                    Id = area.Id,
                    Name = area.Name,
                    DisplayOrder = area.DisplayOrder
                };
            }
        }

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

    #endregion
}
