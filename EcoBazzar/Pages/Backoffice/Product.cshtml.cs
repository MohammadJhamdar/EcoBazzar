using EcoBazzar.DataModel;
using EcoBazzar.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcoBazzar.Pages.Backoffice
{
    public class ProductModel : PageModel
    {
        private IProductServices Services;
        public ProductModel(IProductServices services)
        {
            Services = services;
        }
        public List<Product> Products { get; set; }
        public Product Product { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Products=await Services.GetAllProducts();
            return Page();
        }
        public async Task<IActionResult> OnGetById(int id)
        {
            Product=await Services.GetProductById(id);
            return Page();
        }
    }
}
