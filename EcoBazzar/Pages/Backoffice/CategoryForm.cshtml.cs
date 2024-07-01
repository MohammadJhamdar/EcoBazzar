using EcoBazzar.BindingModel.Category;
using EcoBazzar.Services.CategoryServices;
using EcoBazzar.ViewModel.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcoBazzar.Pages.Backoffice
{
    public class CategoryFormModel : PageModel
    {
        private readonly ICategoryServices categoryServices;
        public CategoryFormModel(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        public CategoryViewModel category {  get; set; }
        [BindProperty]
        public CategoryBindinModel binding { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (id.HasValue)
            {
                var z = await categoryServices.UpdateCategory(binding,(int)id);
                return RedirectToPage("Category", new { handler = "All" });
            }
            var x = await categoryServices.CreateCategory(binding);
            return RedirectToPage("Category", new { handler = "ById", id = x });
        }
    }
}
