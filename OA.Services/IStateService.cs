using OA.Core.Domain;

namespace OA.Services;

public interface IStateService
{
    Task<State> GetStateByIdAsync(int id);
    Task<IEnumerable<State>> GetAllStatesAsync();
}
