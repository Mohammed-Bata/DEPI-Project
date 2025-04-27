using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
