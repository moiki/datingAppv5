using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class SeedService
{
    public static async Task SeedUser(DataContext context)
    {
        if (await context.Users.AnyAsync()) return;
        var jsonData = await File.ReadAllTextAsync("Data/UserSeed.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(jsonData);
        foreach (var seedUser in users) 
        {
            using var hmac = new HMACSHA512();
            seedUser.PaswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            seedUser.PaswordSalt = hmac.Key;
            // seedUser.BirthDate = 
            Console.WriteLine(seedUser.BirthDate.GetType());

            context.Users.Add(seedUser);
        }

        context.SaveChangesAsync();
    }
}