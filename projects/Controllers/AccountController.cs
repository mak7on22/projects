using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projects.Models;
using projects.Servises;
using projects.ViewModels;

namespace projects.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailSendler _emailSender;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            EmailSendler emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                Email = model.Email,
                UserName = model.Nickname,
                Login = model.Nickname,
                DisplayName = model.Nickname,
                IsConfirmed = true,
                RoleName = "User"
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
                return View(model);

            User? user = null;
            if (model.EmailOrNickname.Contains('@'))
            {
                user = await _userManager.FindByEmailAsync(model.EmailOrNickname);
            }
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.EmailOrNickname);
            }
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName!, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction("Index", "Home", new { area = "" });
                return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var newPassword = Guid.NewGuid().ToString("N").Substring(0, 8) + "!";
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (result.Succeeded)
                {
                    await _emailSender.SendEmailAsync(model.Email, "New password", $"Your new password: {newPassword}");
                }
            }
            ViewData["Message"] = "If the email exists, a new password was sent.";
            return View();
        }
    }
}
