using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechNotes.Infrastructure.Users;

namespace TechNotes.Controllers
{
    [Route("account")]
    public class AcoountController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AcoountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("external-login")]
        public IActionResult ExternalLogin(string provider)
        {
            var reiderUrl = Url.Action(nameof(HandleExternalCallBack));
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, reiderUrl);
            return Challenge(properties, provider);
        }

        [AllowAnonymous]
        [HttpGet("external-callback")]
        public async Task<IActionResult> HandleExternalCallBack()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return RedirectWithError("Error loading external login information.");
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);

            if (signInResult.Succeeded)
            {
                return Redirect("/notes");
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (!string.IsNullOrEmpty(email))
                {
                    var user = await _userManager.FindByEmailAsync(email) ?? new User { UserName = email, Email = email, EmailConfirmed = true };

                    await _userManager.CreateAsync(user);
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("/notes");

                }
                return RedirectWithError("Error creating user account.");
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/notes");
        }

        private IActionResult RedirectWithError(string errorMessage)
        {
            var endoded = Uri.EscapeDataString(errorMessage);
            return Redirect($"/register?error={endoded}");
        }
    }
}
