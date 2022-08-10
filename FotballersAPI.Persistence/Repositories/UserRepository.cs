using FotballersAPI.Application.Interfaces;
using FotballersAPI.Application.Interfaces.QueryOptions;
using FotballersAPI.Domain.Data;
using FotballersAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FotballersAPI.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User, GetUserOptions>, IUserRepository
    {

        public UserRepository(FotballerDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckEmailExistsInDatabaseAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<bool> CheckLoginExistsInDatabaseAsync(string username, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(x => x.Username == username, cancellationToken);
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == login, cancellationToken);
        }
    }
}
