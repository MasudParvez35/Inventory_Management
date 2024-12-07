using OA.Core.Domain;
using OA_WEB.Areas.Admin.Models;

namespace OA_WEB.Areas.Admin.Factories;

public interface IAreaModelFactory
{
    Task<AreaListModel> PrepareAreaListModelAsync();
    Task<AreaModel> PrepareAreaModelAsync(AreaModel model, Area area);

    Task<CityListModel> PrepareCityListModelAsync();
    Task<CityModel> PrepareCityModelAsync(CityModel model, City city);

    Task<StateListModel> PrepareStateListModelAsync();
    Task<StateModel> PrepareStateModelAsync(StateModel model, State state);
}
