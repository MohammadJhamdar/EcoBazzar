using System.ComponentModel.DataAnnotations;

namespace EcoBazzar.ViewModel.SubCategory
{
    public class SubCategoryViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "SubCategoryName")]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
