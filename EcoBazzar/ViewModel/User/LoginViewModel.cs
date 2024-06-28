using System.ComponentModel.DataAnnotations;

namespace EcoBazzar.ViewModel.User
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [MaxLength(16)]
        public string Password { get; set; }
    }
}
