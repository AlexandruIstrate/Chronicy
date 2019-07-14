using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Chronicy.Website.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Chronicy.Website.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ChronicyUser> signInManager;
        private readonly UserManager<ChronicyUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        [BindProperty]
        public InputData Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputData
        {
            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public RegisterModel(UserManager<ChronicyUser> userManager,
                             SignInManager<ChronicyUser> signInManager,
                             ILogger<RegisterModel> logger,
                             IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (returnUrl == null)
            {
                returnUrl = Url.Content("~/");
            }

            ChronicyUser user = new ChronicyUser { UserName = Input.Username, Email = Input.Email };
            IdentityResult result = await userManager.CreateAsync(user, Input.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return Page();
            }

            logger.LogInformation("User created a new account with password.");

            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string callbackUrl = Url.Page(
                WebsitePaths.ConfirmEmail,
                pageHandler: null,
                values: new { userId = user.Id, token = token },
                protocol: Request.Scheme);

            // TODO: Improve this
            await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                $"Please confirm your account by <a href='{ HtmlEncoder.Default.Encode(callbackUrl) }'>clicking here</a>.");

            return LocalRedirect(returnUrl);
        }
    }
}