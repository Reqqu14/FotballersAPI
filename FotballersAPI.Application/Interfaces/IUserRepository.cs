﻿using FotballersAPI.Application.Interfaces.QueryOptions;
using FotballersAPI.Domain.Data;

namespace FotballersAPI.Application.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User, GetUserOptions>
    {
        Task<bool> CheckLoginExistsInDatabaseAsync(string username, CancellationToken cancellationToken);

        Task<bool> CheckEmailExistsInDatabaseAsync(string email, CancellationToken cancellationToken);

        Task<bool> CheckUserExistsInDatabaseAsync(int userId, CancellationToken cancellationToken);

        Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken);

        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
