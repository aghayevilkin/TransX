using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TransX.Data;
using TransX.Models;
using TransX.ViewModels;

namespace TransX.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Where(u => u.Id != userId).Take(9).ToList();

            List<BlogTag> tags = _context.BlogTags.ToList();
            ViewBag.Tags = tags;
            VmProfile model = new VmProfile()
            {
                Posts = _context.Blogs.Include(u => u.User).Include(tp => tp.TagToBlogs).ThenInclude(t => t.Tag).OrderByDescending(o => o.AddedDate).ToList(),
                Tags = _context.BlogTags.Include(b => b.TagToBlogs).ThenInclude(bl => bl.Blog).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                User = customUsers,
                UserS = customUserS,
            };
            return View(model);
        }



        public async Task<IActionResult> Login(string returnUrl)
        {
            VmBase model = new VmBase()
            {
                Setting = _context.Settings.FirstOrDefault(),
                ReturnUrl = returnUrl,
                ExternalLogins=(await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Login(VmBase model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.LoginViewModel.Email);

                if (user != null && !user.EmailConfirmed &&
                            (await _userManager.CheckPasswordAsync(user, model.LoginViewModel.Password)))
                {
                    Notify("Email not confirmed yet!", notificationType: NotificationType.error);
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");



                    var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));


                    //Sending mail
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("transxmanagement@gmail.com", "TransX Managements Confirm Email");
                    mail.To.Add(user.Email);
                    mail.Body = "<h1>Hi Bro</h1>" +
                        "<p>For Confirm Email please visit the link below</p>" +
                        "<a href='https://localhost:44374/account/ConfirmEmail?userId=" + user.Id + "&token=" + token + "'>Confirm Email</a>";
                    mail.IsBodyHtml = true;
                    mail.Subject = "Confirm Email";

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("transxmanagement@gmail.com", "lhieoyaivmdladfi");

                    smtpClient.Send(mail);



                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.LoginViewModel.Email, model.LoginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "account");

                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect");
                }

            }
            return View(model);
        }



        public async Task<IActionResult> Register()
        {
            VmBase model = new VmBase()
            {
                Setting = _context.Settings.FirstOrDefault(),
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Register(VmBase model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var customUser = new CustomUser() { Name = model.RegisterViewModel.Name, Surname = model.RegisterViewModel.Surname, UserName = model.RegisterViewModel.Email, Email = model.RegisterViewModel.Email };
                var result = await _userManager.CreateAsync(customUser, model.RegisterViewModel.Password);
                await _userManager.AddToRoleAsync(customUser, "Customer");

                if (result.Succeeded)
                {

                    var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(customUser));


                    //Sending mail
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("transxmanagement@gmail.com", "TransX Managements Confirm Email");
                    mail.To.Add(customUser.Email);
                    mail.Body = "<h1>Hi Bro</h1>" +
                        "<p>For Confirm Email please visit the link below</p>" +
                        "<a href='https://localhost:44374/account/ConfirmEmail?userId=" + customUser.Id + "&token=" + token + "'>Confirm Email</a>";
                    mail.IsBodyHtml = true;
                    mail.Subject = "Confirm Email";

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("transxmanagement@gmail.com", "lhieoyaivmdladfi");

                    smtpClient.Send(mail);
                    //End of sending mail


                    Notify("Send Email Link successfully");


                    return RedirectToAction("login", "account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);

            var userHasPassword = await _userManager.HasPasswordAsync(user);

            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Where(u => u.Id != userId).Take(9).ToList();

            if (!userHasPassword)
            {
                return RedirectToAction("AddPassword");
            }

            VmProfile model = new VmProfile()
            {
                Setting = _context.Settings.FirstOrDefault(),
                User = customUsers,
            };
            return View(model);
        }
        [Authorize]
        public IActionResult ChangePasswordConfirmation()
        {
            VmChangePassword model = new VmChangePassword()
            {
                Setting = _context.Settings.FirstOrDefault(),
            };
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(VmProfile model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;

            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Where(u => u.Id != userId).Take(9).ToList();

            model.User = customUsers;

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user,
                    model.VmChangePassword.CurrentPassword, model.VmChangePassword.NewPassword);

                if (!result.Succeeded)
                {
                    Notify("The password has not been changed!", notificationType: NotificationType.error);
                    ModelState.AddModelError("", "Current Password or New Password incorrect");
                }
                await _signInManager.RefreshSignInAsync(user);
                if (result.Succeeded)
                {
                    Notify("Password Changed successfully");
                    model.VmChangePassword.IsSuccess = true;
                }
                //return RedirectToAction("ChangePasswordConfirmation", "account");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> AddPassword()
        {
            var user = await _userManager.GetUserAsync(User);

            var userHasPassword = await _userManager.HasPasswordAsync(user);

            if (userHasPassword)
            {
                return RedirectToAction("ChangePassword");
            }
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Where(u => u.Id != userId).Take(9).ToList();

            VmProfile model = new VmProfile()
            {
                Setting = _context.Settings.FirstOrDefault(),
                User = customUsers,
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPassword(VmProfile model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;

            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Where(u => u.Id != userId).Take(9).ToList();
            model.User = customUsers;

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.AddPasswordAsync(user, model.VmAddPassword.NewPassword);

                if (!result.Succeeded)
                {
                    Notify("The password has not been added!", notificationType: NotificationType.error);
                    ModelState.AddModelError("", "New Password incorrect");
                }

                await _signInManager.RefreshSignInAsync(user);

                if (result.Succeeded)
                {
                    Notify("Password Add successfully");
                }
                return RedirectToAction("ChangePassword", "account");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                new { ReturnUrl = returnUrl });
            var properties = _signInManager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult>
            ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/account/index");

            VmBase loginViewModel = new VmBase
            {
                Setting = _context.Settings.FirstOrDefault(),
                ReturnUrl = returnUrl,
                ExternalLogins =
                        (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState
                    .AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState
                    .AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }



            // Get the email claim from external login provider (Google, Facebook etc)
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            IdentityUser user = null;

            if (email != null)
            {
                user = await _userManager.FindByEmailAsync(email);

                if (user != null && !user.EmailConfirmed)
                {
                    Notify("Email not confirmed yet!", notificationType: NotificationType.error);
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View("Login", loginViewModel);
                }
            }



            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {

                if (email != null)
                {

                    if (user == null)
                    {
                        user = new CustomUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Name= info.Principal.FindFirstValue(ClaimTypes.GivenName),
                            Surname= info.Principal.FindFirstValue(ClaimTypes.Surname),
                        };

                        await _userManager.CreateAsync(user);

                        var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));


                        //Sending mail
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("transxmanagement@gmail.com", "TransX Managements Confirm Email");
                        mail.To.Add(user.Email);
                        mail.Body = "<h1>Hi Bro</h1>" +
                            "<p>For Confirm Email please visit the link below</p>" +
                            "<a href='https://localhost:44374/account/ConfirmEmail?userId=" + user.Id + "&token=" + token + "'>Confirm Email</a>";
                        mail.IsBodyHtml = true;
                        mail.Subject = "Confirm Email";

                        SmtpClient smtpClient = new SmtpClient();
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.EnableSsl = true;
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new NetworkCredential("transxmanagement@gmail.com", "lhieoyaivmdladfi");

                        smtpClient.Send(mail);
                        //End of sending mail


                        Notify("Send Email Link successfully");


                        return RedirectToAction("login", "account");

                    }

                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on Ilkinga@code.edu.az";

                return View("Error");
            }
        }




        public IActionResult ForgotPassword()
        {
            VmForgotPassword model = new VmForgotPassword() 
            {
                Setting=_context.Settings.FirstOrDefault(),
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(VmForgotPassword model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null /*&& await _userManager.IsEmailConfirmedAsync(user)*/)
                {
                    // Generate the reset password token
                    var token = HttpUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));


                    //Sending mail
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("transxmanagement@gmail.com", "TransX Managements Reset Password");
                    mail.To.Add(model.Email);
                    mail.Body = "<h1>Hi Bro</h1>" +
                        "<p>For resetting password please visit the link below</p>" +
                        "<a href='https://localhost:44374/account/resetpassword?email=" + model.Email + "&token=" + token + "'>Reset password</a>";
                    mail.IsBodyHtml = true;
                    mail.Subject = "Reset password";

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential("transxmanagement@gmail.com", "lhieoyaivmdladfi");

                    smtpClient.Send(mail);
                    //End of sending mail


                    Notify("Send Email Link successfully");
                }

                return RedirectToAction("login", "account");
            }

            return View(model);
        }






        public IActionResult ResetPassword(string token, string email)
        {

            if (token == null || email == null)
            {
                Notify("Invalid password reset token!", notificationType: NotificationType.error);
                ModelState.AddModelError("", "Invalid password reset token");
            }

            VmResetPassword model = new VmResetPassword()
            {
                Setting = _context.Settings.FirstOrDefault(),
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(VmResetPassword model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {

                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        Notify("Password Reset successfully");
                        return RedirectToAction("login", "account");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }


                return RedirectToAction("login", "account");
            }

            return View(model);
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                Notify("UserId or Token Invalid!", notificationType: NotificationType.error);
                return RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {

                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            VmBase model = new VmBase()
            {
                Setting = _context.Settings.FirstOrDefault(),
            };

            if (result.Succeeded)
            {
                Notify("Confirm Email successfully");
                return View(model);
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }




        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
