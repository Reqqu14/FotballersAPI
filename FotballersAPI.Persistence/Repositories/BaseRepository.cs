using FotballersAPI.Application.Interfaces;
using FotballersAPI.Application.Interfaces.QueryOptions;
using FotballersAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FotballersAPI.Persistence.Repositories
{
    public class BaseRepository<Tentity, Toptions> : IAsyncRepository<Tentity, Toptions>
        where Tentity : class
        where Toptions : BaseOptions
    {
        protected readonly FotballerDbContext _dbContext;

        public BaseRepository(FotballerDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Tentity> AddAsync(Tentity entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<Tentity>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(Tentity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<Tentity>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Tentity>> GetAllAsync(Toptions options, CancellationToken cancellationToken)
        {
            IQueryable<Tentity> query = PrepareQueryString(options);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<Tentity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<Tentity>()
                .FindAsync(id, cancellationToken);
        }

        public async Task<Tentity> UpdateAsync(Tentity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<Tentity>().Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        private IQueryable<Tentity> PrepareQueryString(Toptions options)
        {
            var query = _dbContext.Set<Tentity>()
                .AsQueryable()
                .AsNoTracking();

            if(options.Skip.HasValue)
            {
                query = query.Skip(options.Skip.Value);
            }

            if(options.Take.HasValue)
            {
                query = query.Take(options.Take.Value);
            }

            return query;
        }
    }
}
