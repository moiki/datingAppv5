using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController: BaseApiController
{
    private readonly DataContext _context;

    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDto newUser)
    {
        try
        {
            if (await UserExists(newUser.UserName)) return BadRequest("User already exists");
           
            using var hmac = new HMACSHA512();
            var user = new AppUser()
            {
                UserName = newUser.UserName,
                PaswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newUser.Password)),
                PaswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AppUser>> Login(LoginDto login)
    {
        try
        {
            var foundUser = await _context.Users
                .SingleOrDefaultAsync(user => user.UserName == login.UserName);
            if (foundUser == null) return Unauthorized("Invalid Credentials");
            using var hmac = new HMACSHA512(foundUser.PaswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            if (computedHash.Where((t, i) => t != foundUser.PaswordHash[i]).Any())
            {
                return Unauthorized("Invalid Password");
            }

            return foundUser;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }
    }

    private async Task<bool> UserExists(string username)
    {
        try
        {
            return await _context.Users.AnyAsync(user => user.UserName == username);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}