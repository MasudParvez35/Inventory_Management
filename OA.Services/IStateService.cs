using OA.Core.Domain;

namespace OA.Services;

public interface IStateService
{
    Task<State> GetStateByIdAsync(int id);

    Task<IEnumerable<State>> GetAllStatesAsync();

    Task InsertStateAsync(State state);

    Task UpdateStateAsync(State state);

    Task DeleteStateAsync(State state);

    Task<State> GetStateByCityIdAsync(int cityId);
}
