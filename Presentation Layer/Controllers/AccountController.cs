using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Helpers;
using Presentation_Layer.ViewModels;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region Register
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (ModelState.IsValid)
			{
				var MappedUser = new ApplicationUser()
				{
					UserName = registerViewModel.Email.Split("@")[0],
					Email = registerViewModel.Email
					,
					Fname = registerViewModel.Fname,
					Lname = registerViewModel.Lname,
					IsAgree = registerViewModel.IsAgree,
				};
				var Result = await _userManager.CreateAsync(MappedUser, registerViewModel.Password);
				if (Result.Succeeded)
					return RedirectToAction("Login");
				else
					foreach (var error in Result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(registerViewModel);
		}
		#endregion

		#region Login
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					var Flag = await _userManager.CheckPasswordAsync(User, model.Password);
					if (Flag)
					{
						var Result = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						if (Result.Succeeded)
							return RedirectToAction(nameof(HomeController.Index), "Home");
					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(model);
		}
		#endregion

		#region SignOut
		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion

		#region ForgetPassword
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendPasswordResetLink(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
				var ResetPasswordUrl = Url.Action("ResetPassword", "Account", new { email=model.Email,Token},Request.Scheme);
				if (user is not null)
				{
					var email = new Email()
					{
						Subject = "Reset Your Password",
						Recipients = model.Email,
						Body = ResetPasswordUrl
					};
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Invalid Email");
			}
			return View(nameof(ForgetPassword),model);
		}
		#endregion
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		public IActionResult ResetPassword(string email,string Token)
		{
			TempData["Email"] = email; 
			TempData["Token"] = Token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string Email = TempData["Email"] as string;
				string Token = TempData["Token"] as string;
				var user =await _userManager.FindByEmailAsync(Email);
				var result =await _userManager.ResetPasswordAsync(user,Token,model.NewPassword);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, "Invalid Password");
            }
			return View(model);
		}
	}
}
