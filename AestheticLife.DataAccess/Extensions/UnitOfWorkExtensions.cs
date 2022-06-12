using AestheticLife.DataAccess.Domain.Models;
using AestheticsLife.DataAccess.Abstractions;

namespace AestheticLife.DataAccess.Extensions;

public static class UnitOfWorkExtensions
{
    public static async Task<IEnumerable<string>> GetUserRolesAsync(this IUnitOfWork unitOfWork, User user)
    {
        var userRolesRepository = unitOfWork.GetReadonlyRepository<UserRole>();
        var userRoles = await userRolesRepository.GetAsync(
            userRole => userRole.UserId == user.Id,
            null,
            ur => ur.Role);
        var roleNames = userRoles.Select(userRole => userRole.Role.Name).ToList();

        return roleNames;
    }
}