using System.ComponentModel.DataAnnotations;

namespace EcoBazzar.ViewModel.Category
{
    public class CategoryViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "CategoryName")]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
