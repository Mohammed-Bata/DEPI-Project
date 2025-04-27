using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties can be added here
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        public string? Country { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
