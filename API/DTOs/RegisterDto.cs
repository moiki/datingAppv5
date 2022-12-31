using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] public string UserName { get; set; }

        [Required] public string KnownAs { get; set; }
        [Required] public string Gender { get; set; }
        [Required] public DateTime? DateOfBirth { get; set; } // optional to make required work!
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }

    public class UserAccountResponseDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string KnownAs { get; set; }
        public string Gender { get; set; }
    }
}