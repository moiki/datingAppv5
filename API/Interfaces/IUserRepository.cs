using System.Linq.Expressions;
using API.DTOs;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllChanges();
    Task<IEnumerable<AppUser>> ListUsersAsync();
    Task<AppUser?> FindUserByIdAsync(int id);
    Task<MemberDto?> FindUserBy(Expression<Func<AppUser,bool>> where);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
}