using Microsoft.AspNetCore.Mvc;
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

    public async Task<IActionResult> StateEdit(int id)
    {
        var state = await _stateService.GetStateByIdAsync(id);
        if (state == null)
            return RedirectToAction("StateList");

        var model = await _areaModelFactory.PrepareStateModelAsync(null, state);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> StateEdit(StateModel model)
    {
        if (ModelState.IsValid)
        {
            var state = await _stateService.GetStateByIdAsync(model.Id);
            if (state == null)
                return RedirectToAction("StateList");

            state.Name = model.Name;
            state.DisplayOrder = model.DisplayOrder;

            await _stateService.UpdateStateAsync(state);

            return RedirectToAction("StateList");
        }

        model = await _areaModelFactory.PrepareStateModelAsync(model, null);

        return View(model);
    }

    public async Task<IActionResult> StateDelete(int id)
    {
        var state = await _stateService.GetStateByIdAsync(id);
        if (state == null)
            return RedirectToAction("StateList");

        await _stateService.DeleteStateAsync(state);

        return RedirectToAction("StateList");
    }

    #endregion

    #region City

    public async Task<IActionResult> CityList(int id)
    {
        if (id == 0)
            return RedirectToAction("StateList");

        var state = await _stateService.GetStateByIdAsync(id);
        var model = await _areaModelFactory.PrepareCityListModelAsync(state);

        return View(model);
    }

    public async Task<IActionResult> CityCreate(int id)
    {
        var state = await _stateService.GetStateByIdAsync(id);
        if (state == null)
            return RedirectToAction("StateList");

        var model = await _areaModelFactory.PrepareCityModelAsync(new CityModel(), null);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CityCreate(CityModel model)
    {
        var state = await _stateService.GetStateByIdAsync(model.Id);
        if (state == null)
            return RedirectToAction("StateList");

        if (ModelState.IsValid)
        {
            var city = new City()
            {
                StateId = state.Id,
                Name = model.Name,
                DisplayOrder = model.DisplayOrder
            };

            await _cityService.InsertCityAsync(city);

            return RedirectToAction("StateEdit", new {id = state.Id});
        }

        model = await _areaModelFactory.PrepareCityModelAsync(model, null);

        return View(model);
    }

    public async Task<IActionResult> CityEdit(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        if (city == null)
            return RedirectToAction("CityList");

        var model = await _areaModelFactory.PrepareCityModelAsync(null, city);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CityEdit(StateModel model)
    {
        if (ModelState.IsValid)
        {
            var state = await _stateService.GetStateByIdAsync(model.Id);
            if (state == null)
                return RedirectToAction("StateList");

            state.Name = model.Name;
            state.DisplayOrder = model.DisplayOrder;

            await _stateService.UpdateStateAsync(state);

            return RedirectToAction("StateList");
        }

        model = await _areaModelFactory.PrepareStateModelAsync(model, null);

        return View(model);
    }

    public async Task<IActionResult> CityDelete(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);
        if (city == null)
            return RedirectToAction("CityList");

        await _cityService.DeleteCityAsync(city);

        return RedirectToAction("CityList");
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
        var city = await _cityService.GetCityByIdAsync(model.Id);
        if (city == null)
            return RedirectToAction("AreaList");

        if (ModelState.IsValid)
        {
            var area = new Area()
            {
                CityId = city.Id,
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
