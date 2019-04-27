using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouxAcademy.Data;
using RouxAcademy.ViewModels;

namespace RouxAcademy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly StudentDataContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager
            , StudentDataContext context
            , SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }


        /// <summary>
        /// controlls users register actions
        /// </summary>
        /// <returns></returns>
        #region Register Methods

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                //await _context.SaveChangesAsync();

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(
                            string.Empty, error.Description);
                    }
                }
            }

            // if we got this far and somthing failed redisplay form
            return View();
        }
        #endregion


        /// <summary>
        /// controlls users login actions
        /// </summary>
        /// <returns></returns>
        #region Login Methods

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email,
                    model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //if (Url.IsLocalUrl(returnUrl))
                    //{
                    //    return Redirect(returnUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToAction(nameof(StudentController.Index), "Student");
                    //}
                    return RedirectToAction(nameof(StudentController.Index), "Student");
                 }
                else
                {
                    ModelState.AddModelError(string.Empty,
                       "Invalid Login Attempt");
                }
            }

            // if we got this far and somthing went wrong redisplay form
            return View();
        }
        #endregion

        /// <summary>
        /// controlls user signout Action 
        /// </summary>
        /// <returns></returns>
        #region logOut

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion

        #region Access Denied

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


        #endregion
    }
}