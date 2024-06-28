using EcoBazzar.BindingModel.User;
using EcoBazzar.DataModel;
using EcoBazzar.Services.UserServices;
using EcoBazzar.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EcoBazzar.Pages.Account_Page
{
    public class Login : PageModel
    {
        private IUserServices _userServices;
        public Login(IUserServices userServices)
        {
            _userServices = userServices;
        }
        public LoginViewModel user {  get; set; }
        [BindProperty]
        public LoginBindingModel binding { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {

            var x = await _userServices.AuthenticateUser(binding.UserName, binding.Password);
            if(x==null)
            {
                ModelState.AddModelError("UnAuthenticte", "Username or Password is incorrect");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Index");

        }
    }
}
