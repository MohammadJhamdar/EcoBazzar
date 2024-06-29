using System.ComponentModel.DataAnnotations;

namespace EcoBazzar.ViewModel.Product
{
    public class ProductViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        [Range(0, 1000000)]
        public double Price { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        [StringLength(100)]
        public string Brand { get; set; }
        [Required]
        [Range(0, 5)]
        public double Rating { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        [Range(1, 1000)]
        public int Stock { get; set; }
        [Range(1, 100)]
        public int? Discount { get; set; }
       
        [Required]
        public int SubCategoryId { get; set; }
    }
}
