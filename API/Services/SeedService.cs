using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class SeedService
{
    public static async Task SeedUser(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;
        var jsonData = await File.ReadAllTextAsync("Data/UserSeed.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(jsonData);
        var roles = new List<AppRole>
        {
            new AppRole{Name = "Member"},
            new AppRole{Name = "Admin"},
            new AppRole{Name = "Moderator"},
        };
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
        foreach (var seedUser in users)
        {
            seedUser.UserName = seedUser.UserName.ToLower();
            await userManager.CreateAsync(seedUser, "Pa$$w0rd");
            await userManager.AddToRoleAsync(seedUser, "Member");
        }

        var admin = new AppUser
        {
            UserName = "Administrator", 
            Gender = "Male", 
            Email = "admin@datingApp.com",
            City = "Gotham",
            Country = "USA",
            KnownAs = "Batman",
            Interests = "No interests",
            Introduction = "I am the revenge.",
        };
        await userManager.CreateAsync(admin, "Pa$$w0rd");
        await userManager.AddToRolesAsync(admin, new []{"Admin", "Moderator"});
    }
}