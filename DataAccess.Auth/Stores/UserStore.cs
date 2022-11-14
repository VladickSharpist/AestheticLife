using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using DataAccess.Auth.Abstractions.Models;
using DataAccess.Auth.Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Stores;

public class UserStore : IUserRoleStore<AuthUser>, IUserEmailStore<AuthUser>, IUserPasswordStore<AuthUser>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task<string> GetUserIdAsync(AuthUser user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.Id.ToString());

    public Task<string> GetUserNameAsync(AuthUser user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.UserName);

    public Task SetUserNameAsync(AuthUser user, string userName, CancellationToken cancellationToken = default)
    {
        user.UserName = userName;

        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedUserNameAsync(AuthUser user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.NormalizedUserName);
    
    public Task SetNormalizedUserNameAsync(
        AuthUser user,
        string normalizedName,
        CancellationToken cancellationToken = default)
    {
        user.NormalizedUserName = normalizedName;

        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(AuthUser user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<AuthUser>();
        await repository.SaveAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(AuthUser user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<AuthUser>();
        await repository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(AuthUser user, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<AuthUser>();
        await repository.RemoveAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<AuthUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var id = long.Parse(userId);

        return (await _unitOfWork
            .GetReadonlyRepository<AuthUser>()
            .GetAsync(u => u.Id == id))
            .FirstOrDefault();
    }

    public async Task<AuthUser> FindByNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return (await _unitOfWork
                .GetReadonlyRepository<AuthUser>()
                .GetAsync(u => u.UserName == userName))
            .FirstOrDefault();
    }

    public async Task AddToRoleAsync(AuthUser user, string roleName, CancellationToken cancellationToken = default)
    {
        var rolesRepository = _unitOfWork.GetReadonlyRepository<Role>();
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var role = (await rolesRepository.GetAsync(role => role.Name == roleName)).FirstOrDefault();

        var userRole = new UserRole
        {
            RoleId = role.Id,
            UserId = user.Id
        };

        await userRolesRepository.SaveAsync(userRole);
    }

    public async Task RemoveFromRoleAsync(AuthUser user, string roleName, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var userRole = (await userRolesRepository.GetAsync(
            userRole => userRole.Role.Name == roleName && userRole.UserId == user.Id)).FirstOrDefault();

        await userRolesRepository.RemoveAsync(userRole);
    }

    public async Task<IList<string>> GetRolesAsync(AuthUser user, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var userRoles = await userRolesRepository.GetAsync(
            userRole => userRole.UserId == user.Id,
            null,
            ur => ur.Role);
        var roleNames = userRoles.Select(userRole => userRole.Role.Name).ToList();

        return roleNames;
    }

    public async Task<bool> IsInRoleAsync(AuthUser user, string roleName, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var isInRole = (await userRolesRepository.GetAsync(
            userRole => userRole.Role.Name == roleName && userRole.UserId == user.Id)).Any();

        return isInRole;
    }

    public async Task<IList<AuthUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        var userRolesRepository = _unitOfWork.GetCustomRepository<UserRole, IUserRoleRepository>();
        var userRoles = await userRolesRepository.GetAsync(
            userRole => userRole.Role.NormalizedName == roleName,
            null,
            ur => ur.User);
        var users = userRoles.Select(userRole => userRole.User).ToList();

        return users;
    }

    public void Dispose()
    {
        // is not needed
    }

    public Task SetEmailAsync(AuthUser user, string email, CancellationToken cancellationToken = default)
    {
        user.Email = email;

        return Task.CompletedTask;
    }

    public Task<string> GetEmailAsync(AuthUser user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.Email);

    public Task<bool> GetEmailConfirmedAsync(AuthUser user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.EmailConfirmed);

    public Task SetEmailConfirmedAsync(AuthUser user, bool confirmed, CancellationToken cancellationToken = default)
    {
        user.EmailConfirmed = confirmed;

        return Task.CompletedTask;
    }

    public async Task<AuthUser> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        => (await _unitOfWork
            .GetReadonlyRepository<AuthUser>()
            .GetAsync(u => u.Email == email))
            .FirstOrDefault();

    public Task<string> GetNormalizedEmailAsync(AuthUser user, CancellationToken cancellationToken)
        => Task.FromResult(user.NormalizedEmail);

    public Task SetNormalizedEmailAsync(
        AuthUser user,
        string normalizedEmail,
        CancellationToken cancellationToken = default)
    {
        user.NormalizedEmail = normalizedEmail;

        return Task.CompletedTask;
    }

    public Task SetPasswordHashAsync(AuthUser user, string passwordHash, CancellationToken cancellationToken = default)
    {
        user.PasswordHash = passwordHash;

        return Task.CompletedTask;
    }

    public Task<string> GetPasswordHashAsync(AuthUser user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.PasswordHash);
    
    public Task<bool> HasPasswordAsync(AuthUser user, CancellationToken cancellationToken = default)
        => Task.FromResult(user.PasswordHash != null);
}