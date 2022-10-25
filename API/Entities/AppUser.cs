using System.ComponentModel.DataAnnotations;
using API.Extensions;

namespace API.Entities;

public class AppUser
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; }
    public byte[] PaswordHash { get; set; }
    public byte[] PaswordSalt { get; set; }
    public DateTime BirthDate { get; set; }
    public string KnownAs { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastActive { get; set; } = DateTime.Now;
    public string Gender { get; set; }
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public ICollection<Photo> Photos { get; set; }

    public int GetAge()
    {
        return BirthDate.GetAge();
    }
}