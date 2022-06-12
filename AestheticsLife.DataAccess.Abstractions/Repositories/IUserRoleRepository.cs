using AestheticLife.DataAccess.Domain.Models;

namespace AestheticsLife.DataAccess.Abstractions;

public interface IUserRoleRepository : IBaseReadonlyRepository<UserRole>
{
    Task SaveAsync(UserRole model);

    Task RemoveAsync(UserRole model);
}