using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Dtos
{
    public class RegisterationRequestDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [PasswordValidation]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
