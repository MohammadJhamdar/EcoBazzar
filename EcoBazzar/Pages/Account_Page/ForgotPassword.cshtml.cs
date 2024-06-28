using EcoBazzar.BindingModel.User;
using EcoBazzar.Services.UserServices;
using EcoBazzar.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcoBazzar.Pages.Account_Page
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IUserServices userServices;
        public ForgotPasswordModel(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        [BindProperty]
        public FPBindingModel binding {  get; set; }
        public FPViewModel email {  get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("../Index");
        }
    }
}
