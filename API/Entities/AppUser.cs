using System.ComponentModel.DataAnnotations;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser: IdentityUser<int>
{
    public DateTime BirthDate { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastActive { get; set; } = DateTime.Now;
    public string Gender { get; set; }
    public string Introduction { get; set; } = "Hi, I am a good looking person from inside.";
    public string LookingFor { get; set; } = "Loving Experiences";
    public string Interests { get; set; } = "I have non specific interests, Let's see how it goes!";
    public string City { get; set; }
    public string Country { get; set; }
    public ICollection<Photo> Photos { get; set; }
    public ICollection<AppUserRole> AppUserRoles { get; set; }
}