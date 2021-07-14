using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TransX.Data;
using TransX.Models;
using TransX.ViewModels;

namespace TransX.Controllers
{
    public class AccountController : Controller
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

                var result = await _signInManager.PasswordSignInAsync(model.LoginViewModel.Email, model.LoginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");

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
                    await _signInManager.SignInAsync(customUser, false);
                    return RedirectToAction("index", "home");
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

            if (!userHasPassword)
            {
                return RedirectToAction("AddPassword");
            }

            VmChangePassword model = new VmChangePassword()
            {
                Setting = _context.Settings.FirstOrDefault(),
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
        public async Task<IActionResult> ChangePassword(VmChangePassword model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Current Password or New Password incorrect");
                }
                await _signInManager.RefreshSignInAsync(user);
                if (result.Succeeded)
                {
                    model.IsSuccess = true;
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

            VmAddPassword model = new VmAddPassword()
            {
                Setting = _context.Settings.FirstOrDefault(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(VmAddPassword model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var result = await _userManager.AddPasswordAsync(user, model.NewPassword);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "New Password incorrect");
                }

                await _signInManager.RefreshSignInAsync(user);

                if (result.Succeeded)
                {
                    model.IsSuccess = true;
                }
                //return RedirectToAction("AddPasswordConfirmation", "account");
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


            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    var user = await _userManager.FindByEmailAsync(email);

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


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
