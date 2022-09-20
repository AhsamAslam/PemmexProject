using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Pemmex.Identity.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Pemmex.Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            RoleManager<ApplicationRole> roleManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            try
            {
                var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
                if (context == null)
                {
                    return Unauthorized(Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized);
                }
                var vm = new LoginViewModel()
                {
                    ReturnUrl = returnUrl
                };
                return View(vm);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                // check if we are in the context of an authorization request
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberLogin, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        if (context != null)
                        {
                            //var data = DefaultDataService.GetDefaultRolesAndScreen(CurrentUser.OrganizationIdentifier);
                            // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                            return Redirect(model.ReturnUrl);
                        }

                        // request for a local page
                        if (Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else if (string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return Redirect("~/");
                        }
                        else
                        {
                            // user might have clicked on a malicious link - should be logged
                            throw new Exception("invalid return URL");
                        }
                    }
                    if (result.RequiresTwoFactor)
                    {
                        //var Url = Path.Combine(Request.Scheme + "://" + Request.Host + "/Account/LoginWith2fa?ReturnUrl=" + model.ReturnUrl);
                        //return Redirect(Url);
                        return RedirectToAction("LoginWith2fa", new { user.Email, model.ReturnUrl });
                    }
                    ModelState.AddModelError(string.Empty, "Invalid credentials");
                }
                

                return View(model);
            }
            catch (Exception e)
            {
                throw;
            }
           
        }

        [HttpGet] 
        public async Task<IActionResult> LoginWith2fa(string Email, string returnUrl = null)
        {
            
            var userM = await _userManager.FindByEmailAsync(Email);

            var token = await _userManager.GenerateTwoFactorTokenAsync(userM, "Email");

            await _emailSender.SendEmailAsync(
                Email,
                "Verification Code",
                $"Please verify your login by:  <h2>{token}</h2>.");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> LoginWith2fa(TwoFactorViewModel twoFactor, string returnUrl)
        {

            var result = await _signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
               
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            try
            {
                //if (User?.Identity.IsAuthenticated == true)
                //{
                //    // delete local authentication cookie
                //    await _signInManager.SignOutAsync();
                //}
                await _signInManager.SignOutAsync();
                var logout = await _interaction.GetLogoutContextAsync(logoutId);

                return Redirect(logout.PostLogoutRedirectUri);
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "USER");

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = await _userManager.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Email, model.Email)
                        });

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                return View("RegistrationSuccess");
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        [HttpGet]
        public IActionResult Role()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Role(RolesViewModel model)
        {
            try
            {
                var role = new ApplicationRole { Name = model.RoleName, NormalizedName = model.RoleName };
                var result = await _roleManager.CreateAsync(role);
                return Redirect("~/");
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
        [HttpGet]
        public IActionResult SendEmailForPasswordReset()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmailForPasswordReset(EmailViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return View("UserNotFound");
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { code },
                protocol: Request.Scheme);
            var callbackUrl1 = Path.Combine(Request.Scheme + "://" + Request.Host + "/Account/ResetPassword?code=" + code);

            await _emailSender.SendEmailAsync(
                model.Email,
                "Reset Password",
                $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl1)}'>clicking here</a>.");

            return View("EmailSendConfirmation");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return View("UserNotFound");
            }
            model.Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordSuccess");
            }
            return View("Login");
        }
        

    }
}
