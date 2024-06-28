using EcoBazzar.BindingModel.User;
using EcoBazzar.Services.UserServices;
using EcoBazzar.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcoBazzar.Pages.Account_Page
{
    public class SignUpModel : PageModel
    {
        private IUserServices _userServices;
        public SignUpModel(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public UserViewModel user { get; set; }
        [BindProperty]
        public UserBindingModel binding { get; set; }

        public string Role { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //binding.Role = "user";
            await _userServices.CreateUSer(binding);
            return RedirectToPage("../Index");
        }
    }
}
