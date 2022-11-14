using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using DataAccess.Auth.Abstractions.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Stores;

public class RoleStore : IRoleStore<Role>
{
    private readonly IUnitOfWork _unitOfWork;


    public RoleStore(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<Role>();
        await repository.SaveAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<Role>();
        await repository.UpdateAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken = default)
    {
        var repository = _unitOfWork.GetReadWriteRepository<Role>();
        await repository.RemoveAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return IdentityResult.Success;
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken = default)
        => Task.FromResult(role.Id.ToString());

    public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken = default)
        => Task.FromResult(role.Name);

    public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken = default)
    {
        role.Name = roleName;

        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken = default)
        => Task.FromResult(role.NormalizedName);

    public Task SetNormalizedRoleNameAsync(
        Role role,
        string normalizedName,
        CancellationToken cancellationToken = default)
    {
        role.NormalizedName = normalizedName;

        return Task.CompletedTask;
    }

    public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var intRoleId = int.Parse(roleId);
        var role = (await _unitOfWork.GetReadWriteRepository<Role>().GetAsync(role => role.Id == intRoleId)).FirstOrDefault();

        return role;
}

    public async Task<Role> FindByNameAsync(string roleName, CancellationToken cancellationToken = default)
        => (await _unitOfWork.GetReadonlyRepository<Role>().GetAsync(role => role.Name == roleName)).FirstOrDefault();

    public void Dispose()
    {
        // is not needed
    }
}