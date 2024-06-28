using System.ComponentModel.DataAnnotations;

namespace EcoBazzar.ViewModel.User
{
    public class FPViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
