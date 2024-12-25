using Microsoft.EntityFrameworkCore;
using OA.Core.Domain;
using OA.Data;

namespace OA.Services;

public class StateService : IStateService
{
    #region Fields

    protected readonly IRepository<State> _stateRepository;
    protected readonly IRepository<City> _cityRepository;

    #endregion

    #region Ctor

    public StateService(IRepository<State> stateRepository, 
        IRepository<City> cityRepository)
    {
        _stateRepository = stateRepository;
        _cityRepository = cityRepository;
    }

    #endregion

    #region Methods

    public async Task InsertStateAsync(State state)
    {
        await _stateRepository.InsertAsync(state);
    }

    public async Task UpdateStateAsync(State state)
    {
        await _stateRepository.UpdateAsync(state);
    }

    public async Task DeleteStateAsync(State state)
    {
        await _stateRepository.DeleteAsync(state);
    }

    public async Task<IEnumerable<State>> GetAllStatesAsync()
    {
        return (await _stateRepository.GetAllAsync())
            .OrderBy(s => s.DisplayOrder);
    }

    public async Task<State> GetStateByIdAsync(int id)
    {
        return await _stateRepository.GetByIdAsync(id);
    }

    public async Task<State> GetStateByCityIdAsync(int cityId)
    {
        if (cityId == 0)
            return null;

        var query = from s in _stateRepository.Table
                    join c in _cityRepository.Table on s.Id equals c.StateId
                    where c.Id == cityId
                    select s;

        return await query.FirstOrDefaultAsync();
    }

    #endregion
}
