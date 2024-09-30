using Linkdev.IKEA.DAL.Entities.Identity;
using Linkdev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.IKEA.PL.Controllers.Identity
{
	public class AccountController : Controller
    {
        #region Services
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        } 

        #endregion

        #region SignUp

        [HttpGet] // GET : "BaseUrl/Account/SignUp"
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is { })
            {
                ModelState.AddModelError(string.Empty, "This Username is already taken by another user");
                return View(model);
            }

            user = new ApplicationUser()
            {
                FirstName = model.UserName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                IsAgreed = model.IsAgreed,
            };

            var CreatedUser = await _userManager.CreateAsync(user, model.Password);

            if (CreatedUser.Succeeded)
                return RedirectToAction(nameof(SignIn));

            foreach (var error in CreatedUser.Errors)
                ModelState.AddModelError(error.Code, error.Description);

            return View(model);
        } 

        #endregion

        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        } 

        #endregion
    }
}
