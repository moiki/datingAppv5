using System.Linq.Expressions;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class UserRepository: IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    public void Update(AppUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AppUser>> ListUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<AppUser?> FindUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser?> FindUserBy(Expression<Func<AppUser,bool>> where)
    {
        return await _context.Users.SingleOrDefaultAsync(where);
    }
}