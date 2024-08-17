using OA.Core.Domain;
using OA.Data;

namespace OA.Services
{
    public class StateService : IStateService
    {
        protected readonly IRepository<State> _stateRepository;

        public StateService(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
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
}
