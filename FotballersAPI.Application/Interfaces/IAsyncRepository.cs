using FotballersAPI.Application.Interfaces.QueryOptions;

namespace FotballersAPI.Application.Interfaces
{
    public interface IAsyncRepository<Tentity, Toptions> 
        where Tentity : class
        where Toptions : BaseOptions
    {
        Task<Tentity> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Tentity>> GetAllAsync(Toptions options, CancellationToken cancellationToken);

        Task<Tentity> AddAsync(Tentity entity, CancellationToken cancellationToken);

        Task<Tentity> UpdateAsync(Tentity entity, CancellationToken cancellationToken);

        Task DeleteAsync(Tentity entity, CancellationToken cancellationToken);       
    }
}
