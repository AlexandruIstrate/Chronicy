using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Chronicy.Website.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Chronicy.Website.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ChronicyUser> signInManager;
        private readonly ILogger<LoginModel> logger;

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; private set; }

        public LoginModel(SignInManager<ChronicyUser> signInManager,
                          ILogger<LoginModel> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        // TODO: Support either username or email
        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool Remember { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (returnUrl == null)
            {
                returnUrl = Url.Content("~/");
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (returnUrl == null)
            {
                returnUrl = Url.Content("~/");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.Remember, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                logger.LogInformation("User login requires two-factor authentication.");
                return RedirectToPage(WebsitePaths.LoginTwoFactor, new { ReturnUrl = returnUrl, RememberMe = Input.Remember });
            }

            if (result.IsLockedOut)
            {
                logger.LogWarning("User account locked out.");
                return RedirectToPage(WebsitePaths.Lockout);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return Page();
        }
    }
}