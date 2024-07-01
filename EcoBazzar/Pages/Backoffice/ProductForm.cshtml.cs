using EcoBazzar.BindingModel.Product;
using EcoBazzar.DataModel;
using EcoBazzar.Services.ProductServices;
using EcoBazzar.Services.SubCategoryServices;
using EcoBazzar.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace EcoBazzar.Pages.Backoffice
{
    public class ProductFormModel : PageModel
    {
        private IProductServices Services { get; set; }
        private ISubCategoryServices SubCategoryServices { get; set; }
        public ProductFormModel(IProductServices services ,ISubCategoryServices subCategoryServices)
        {
            Services = services;
            this.SubCategoryServices = subCategoryServices;
        }
        public ProductViewModel product {  get; set; }
        [BindProperty]
        public ProductBindingModel binding { get; set; }
        public List<SelectListItem> SubCategories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var AllSubCategories = await SubCategoryServices.GetAllSubCategories();
            SubCategories = new List<SelectListItem>();

            foreach (var subCategory in AllSubCategories)
            {
                SubCategories.Add(new SelectListItem
                {
                    Value = subCategory.Id.ToString(),
                    Text = subCategory.Name
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await Services.CreateProduct(binding);
            if (result!=null)
            {
                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToPage("/AdminPage");
            }

            return Page();
        }
    }
}


public class Sub
{
    public int value { get; set; }
    public string Name { get; set; }
    public Sub(int value, string name)
    {
        this.value = value;
        Name = name;
    }
}