using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using DataAccess.Auth.Abstractions.Models;

namespace DataAccess.Auth.Abstractions.Repositories;

public interface IUserRoleRepository : IBaseReadonlyRepository<UserRole>
{
    Task SaveAsync(UserRole model);

    Task RemoveAsync(UserRole model);
}