using EcoBazzar.BindingModel.SubCategory;
using EcoBazzar.ViewModel.SubCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcoBazzar.Pages.Backoffice
{
    public class SubCategoryFormModel : PageModel
    {
        [BindProperty]
        public SubCategoryBindingModel binding { get; set; }

        public SubCategoryViewModel SubCategory {  get; set; }
        public List<SelectListItem> Categories { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Backoffice/SubCategory");
        }

   
    }
}
