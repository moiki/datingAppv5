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
    public static async Task SeedUser(UserManager<AppUser> userManager)
    {
        if (await userManager.Users.AnyAsync()) return;
        var jsonData = await File.ReadAllTextAsync("Data/UserSeed.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(jsonData);
        foreach (var seedUser in users)
        {
            seedUser.UserName = seedUser.UserName.ToLower();
            await userManager.CreateAsync(seedUser, "Pa$$w0rd");
        }
    }
}