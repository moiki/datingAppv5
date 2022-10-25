using System.Linq.Expressions;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<IEnumerable<AppUser>> ListUsersAsync();
    Task<AppUser?> FindUserByIdAsync(int id);
    Task<AppUser?> FindUserBy(Expression<Func<AppUser,bool>> where);
}