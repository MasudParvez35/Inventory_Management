using OA.Core.Domain;
using OA.Data;

namespace OA.Services;

public class StateService : IStateService
{
    protected readonly IRepository<State> _stateRepository;

    public StateService(IRepository<State> stateRepository)
    {
        _stateRepository = stateRepository;
    }

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
        return await _stateRepository.GetAllAsync();
    }

    public async Task<State> GetStateByIdAsync(int id)
    {
        return await _stateRepository.GetByIdAsync(id);
    }
}
