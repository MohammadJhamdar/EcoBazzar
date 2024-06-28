using System.ComponentModel.DataAnnotations;

namespace EcoBazzar.BindingModel.User
{
    public class LoginBindingModel
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
