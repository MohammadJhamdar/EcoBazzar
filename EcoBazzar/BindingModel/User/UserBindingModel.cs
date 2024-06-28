using System.ComponentModel.DataAnnotations;

namespace EcoBazzar.BindingModel.User
{
    public class UserBindingModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        
        public string Role { get; set; }



        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [EmailAddress]

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(16)]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(6)]
        public string Gender { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
