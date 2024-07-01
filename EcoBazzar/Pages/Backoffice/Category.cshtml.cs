using EcoBazzar.BindingModel.Category;
using EcoBazzar.BindingModel.Product;
using EcoBazzar.DataModel;
using EcoBazzar.Services.CategoryServices;
using EcoBazzar.ViewModel.Category;
using EcoBazzar.ViewModel.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcoBazzar.Pages.Backoffice
{
    public class CategoryModel : PageModel
    {
       private readonly ICategoryServices categoryServices;
        public CategoryModel(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }
        public Category cat {  get; set; }
        public List<Category> categories { get; set; }

        public async Task<IActionResult> OnGetAllAsync()
        {
            categories = await categoryServices.GetAllCategories();
            return Page();
        }

        public async Task<IActionResult> OnGetByID(int? id)
        {
            cat = await categoryServices.GetCategoryById(id);
            return Page();
        }
        public async Task<IActionResult> OnGet()
        {
            return RedirectToPage(new { handler = "All" });
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await categoryServices.DeleteCategory(id);
            return RedirectToPage(new { handler = "All" });
        }

    }
}
