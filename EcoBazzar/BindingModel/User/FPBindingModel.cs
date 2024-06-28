using System.ComponentModel.DataAnnotations;

namespace EcoBazzar.BindingModel.User
{
    public class FPBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }
    }
}
