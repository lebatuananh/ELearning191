using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeaLearning.Common;
using PeaLearning.Common.Enumerations;
using PeaLearning.Common.Utils;
using PeaLearning.Domain.AggregateModels.UserAggregate;
using PeaLearning.Website.Models;
using PeaLearning.Website.Models.Account;

namespace PeaLearning.Website.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("dang-nhap", Name = "Login")]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;

            ViewBag.TitlePage = SEO.AddTitle(Constants.Meta.LoginTitle);
            ViewBag.MetaDescription = SEO.AddMeta("description", Constants.Meta.LoginDesc);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("dang-nhap", Name = "Login")]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            CommonResultModel objMsg = new CommonResultModel();
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    objMsg.Error = false;
                    objMsg.Title = returnUrl;
                    objMsg.NextAction = (int)NextAction.Redirect;
                    return Json(objMsg);
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    objMsg.Error = true;
                    objMsg.Title = Constants.Notify.AccountLocked;
                    return Json(objMsg);
                }
                else
                {
                    objMsg.Error = true;
                    objMsg.Title = "Email hoặc mật khẩu không đúng";
                    return Json(objMsg);
                }
            }
            return Json(objMsg);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            // ReSharper disable once Mvc.ViewNotResolved
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("dang-ky")]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewBag.TitlePage = SEO.AddTitle(Constants.Meta.RegisterTitle);
            ViewBag.MetaDescription = SEO.AddMeta("description", Constants.Meta.RegisterDesc);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("dang-ky")]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            CommonResultModel objMsg = new CommonResultModel();
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                objMsg.Error = true;
                objMsg.Title = string.Join("</br>",
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage));
                return Json(objMsg);
            }
            //MM/dd/yyy
            var user = new User(model.Email, model.FirstName, model.LastName, model.Email, string.Empty, false, model.Address, true);
            user.PhoneNumber = model.PhoneNumber;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User created a new account with password.");
                objMsg.Title = "/";
                objMsg.NextAction = (int)NextAction.Redirect;
                return Json(objMsg);
            }

            // If we got this far, something failed, redisplay form
            return Json(objMsg);
        }

        [Route("dang-xuat")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

    }
}
