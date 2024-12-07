using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OA.Core.Domain;
using OA.Services;
using OA_WEB.Areas.Admin.Factories;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Controllers;

[Area("Admin")]
public class AreaController : Controller
{
    #region Fields

    private readonly IAreaService _areaService;
    private readonly IStateService _stateService;
    private readonly ICityService _cityService;
    private readonly IAreaModelFactory _areaModelFactory;

    #endregion

    #region Ctor

    public AreaController(IAreaService areaService,
        IStateService stateService,
        ICityService cityService,
        IAreaModelFactory areaModelFactory)
    {
        _areaService = areaService;
        _stateService = stateService;
        _cityService = cityService;
        _areaModelFactory = areaModelFactory;
    }

    #endregion

    #region Methods

    #region State

    public async Task<IActionResult> StateList()
    {
        var model = await _areaModelFactory.PrepareStateListModelAsync();
        
        return View(model);
    }

    public async Task<IActionResult> StateCreate()
    {
        var model = await _areaModelFactory.PrepareStateModelAsync(new StateModel(), null);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> StateCreate(StateModel model)
    {
        if (ModelState.IsValid)
        {
            var state = new State()
            {
                Name = model.Name,
                DisplayOrder = model.DisplayOrder
            };

            await _stateService.InsertStateAsync(state);

            return RedirectToAction("StateList");
        }

        model = await _areaModelFactory.PrepareStateModelAsync(model, null);

        return View(model);
    }

    #endregion

    #region City

    public async Task<IActionResult> CityList()
    {
        var model = await _areaModelFactory.PrepareCityListModelAsync();

        return View(model);
    }

    public async Task<IActionResult> CityCreate()
    {
        var model = await _areaModelFactory.PrepareCityModelAsync(new CityModel(), null);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CityCreate(CityModel model)
    {
        if (ModelState.IsValid)
        {
            var city = new City()
            {
                Name = model.Name,
                DisplayOrder = model.DisplayOrder
            };

            await _cityService.InsertCityAsync(city);

            return RedirectToAction("CityList");
        }

        model = await _areaModelFactory.PrepareCityModelAsync(model, null);

        return View(model);
    }

    #endregion

    #region Area

    public async Task<IActionResult> AreaList()
    {
        var model = await _areaModelFactory.PrepareAreaListModelAsync();

        return View(model);
    }

    public async Task<IActionResult> AreaCreate()
    {
        var model = await _areaModelFactory.PrepareAreaModelAsync(new AreaModel(), null);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AreaCreate(AreaModel model)
    {
        if (ModelState.IsValid)
        {
            var area = new Area()
            {
                Name = model.Name,
                DisplayOrder = model.DisplayOrder
            };

            await _areaService.InsertAreaAsync(area);

            return RedirectToAction("AreaList");
        }

        model = await _areaModelFactory.PrepareAreaModelAsync(model, null);

        return View(model);
    }

    #endregion

    #endregion
}
