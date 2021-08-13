using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [Authorize]
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();

            List<Social> socials = _context.Socials.ToList();
            socials.Insert(0, new Social() { Id = 0, Icon = "Select" });
            ViewBag.Socials = socials;

            Blog blogdate = _context.Blogs.Where(u=>u.UserId==userId).OrderByDescending(d=>d.AddedDate).LastOrDefault();
            if (blogdate!=null)
            {
                ViewBag.LastPostDate = blogdate.AddedDate;
            }

            

            List<BlogTag> tags = _context.BlogTags.ToList();
            ViewBag.Tags = tags;
            VmProfile model = new VmProfile()
            {
                Posts = _context.Blogs.Include(u => u.User).Include(tp => tp.TagToBlogs).ThenInclude(t => t.Tag).Where(p=>p.UserId== userId).OrderByDescending(o => o.AddedDate).ToList(),
                SavedBlogs = _context.SavedBlogs.Include(pp => pp.Blog).ThenInclude(cat => cat.Category).Include(u => u.User).Where(sa => sa.UserId == userId).OrderByDescending(o => o.AddedDate).ToList(),
                RequestQuotes = _context.RequestQuotes.Include(u => u.User).Include(s => s.Service).Where(us => us.UserId == userId).OrderByDescending(a => a.AddedDate).ToList(),
                Tags = _context.BlogTags.Include(b => b.TagToBlogs).ThenInclude(bl => bl.Blog).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                User = customUsers,
                UserS = customUserS,
            };
            return View(model);
        }


        public IActionResult UpdateUser()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();

            List<BlogTag> tags = _context.BlogTags.ToList();
            ViewBag.Tags = tags;
            VmProfile model = new VmProfile()
            {
                Posts = _context.Blogs.Include(u => u.User).Include(tp => tp.TagToBlogs).ThenInclude(t => t.Tag).OrderByDescending(o => o.AddedDate).ToList(),
                Tags = _context.BlogTags.Include(b => b.TagToBlogs).ThenInclude(bl => bl.Blog).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                User = customUsers,
                UserS = customUserS,
            };
            return View(model);

        }

        [HttpPost]
        public IActionResult UpdateUser(VmProfile model)
        {
            model.Setting = _context.Settings.FirstOrDefault();
            model.Socials = _context.Socials.ToList();
            string userId = _userManager.GetUserId(User);
            model.User.Id = userId;
            
            if (ModelState.IsValid)
            {
                CustomUser customUser = _context.CustomUsers.Find(model.User.Id);
                customUser.Name = model.User.Name;
                customUser.Surname = model.User.Surname;
                customUser.Profision = model.User.Profision;
                customUser.Adress = model.User.Adress;
                customUser.About = model.User.About;

                _context.SaveChanges();

                Notify("Profile Updated");
                return RedirectToAction("index");
                
            }
            Notify("Profile Not Updated!", notificationType: NotificationType.error);

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateUserImage(VmProfile model)
        {
            model.Setting = _context.Settings.FirstOrDefault();
            model.Socials = _context.Socials.ToList();
            string userId = _userManager.GetUserId(User);

                CustomUser customUser = _context.CustomUsers.Find(userId);


                if (model.User.ImageFileTwo != null)
                {
                    if (model.User.ImageFileTwo.ContentType == "image/png" || model.User.ImageFileTwo.ContentType == "image/jpeg" || model.User.ImageFileTwo.ContentType == "image/gif" || model.User.ImageFileTwo.ContentType == "image/svg")
                    {
                        if (model.User.ImageFileTwo.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.User.ImageFileTwo.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Accounts", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.User.ImageFileTwo.CopyTo(stream);
                            }

                            customUser.Image = fileName;

                            _context.SaveChanges();

                            Notify("Image Updated");

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Notify("Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        Notify("Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
                        return RedirectToAction("Index");
                    }
                }


            Notify("You Didn't Choose Image!", notificationType: NotificationType.warning);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult SocialCreate(VmProfile model)
        {
            model.Setting = _context.Settings.FirstOrDefault();
            model.Socials = _context.Socials.ToList();
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(User);
                if (model.SocialToUser.SocialId == 0)
                {
                    ModelState.AddModelError("SocialId", "Social secmelisiniz!");
                    List<Social> socials = _context.Socials.ToList();
                    socials.Insert(0, new Social() { Id = 0, Icon = "Select" });
                    ViewBag.Socials = socials;

                    Notify("You Didn't Choose Social!", notificationType: NotificationType.warning);
                    return RedirectToAction("index");
                }
                model.SocialToUser.UserId = userId;

                _context.SocialToUsers.Add(model.SocialToUser);
                _context.SaveChanges();

                Notify("Added Social");
                return RedirectToAction("index");
            }
            Notify("Social not added!", notificationType: NotificationType.error);
            return RedirectToAction("index");
        }

        public IActionResult SocialDelete(int? id)
        {
            if (id == null)
            {
                Notify("Social not Deleted!", notificationType: NotificationType.error);
                return RedirectToAction("index");
            }

            SocialToUser socialToUser = _context.SocialToUsers.FirstOrDefault(i => i.Id == id);
            if (socialToUser == null)
            {
                Notify("Social not Deleted!", notificationType: NotificationType.error);
                return RedirectToAction("index");
            }

            _context.SocialToUsers.Remove(socialToUser);
            _context.SaveChanges();

            Notify("Social Deleted");

            return RedirectToAction("index");
        }



        public async Task<IActionResult> Login(string returnUrl)
        {
            VmBase model = new VmBase()
            {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
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
            model.Socials = _context.Socials.ToList();
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
                    mail.Body = "<table style = 'Margin:0 auto;background:#f6f6f6;border-collapse:collapse;border-color:transparent;border-spacing:0;float:none;margin:0 auto;padding:0;text-align:center;vertical-align:top;width:100%' >Dear " + user.UserName + "<td height = '50px' style='Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Open Sans,sans-serif;font-size:32px;font-weight:400;line-height:32px;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word'></td></tr></tbody></table><table class='m_4511308450920007087container-radius' style='border-top-width:0;border-top-color:#efefef;border-left-width:0;border-right-width:0;border-bottom-width:1px;border-bottom-color:#efefef;border-right-color:#efefef;border-left-color:#efefef;border-style:solid;border-bottom-left-radius:3px;border-bottom-right-radius:3px;display:table;padding-bottom:50px;border-spacing:60px 0;border-collapse:separate;width:100%;background:#f6f6f6;max-width:800px'><tbody><tr><td><table class='m_4511308450920007087row' style='border-collapse:collapse;border-color:transparent;border-spacing:0;display:table;padding:0;text-align:left;vertical-align:top;width:100%'><tbody><tr style = 'padding:0;text-align:left;vertical-align:top' ></ tr ></ tbody ></ table> Dear " + user.UserName + ",</p><p>Thanks for getting started with our TransX!</p><p>We need a little more information to complete your registration, including a confirmation of your email address. </p><p>Click below to confirm your email address:<br>" +
                            "<a href='https://localhost:44374/account/ConfirmEmail?userId=" + user.Id + "&token=" + token + "'>Confirm Email</a>" +
                        "</td></tr></tbody></table></td></tr></tbody></table>";
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
                Socials = _context.Socials.ToList(),
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Register(VmBase model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            model.Socials = _context.Socials.ToList();
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
                    mail.Body = "<table style = 'Margin:0 auto;background:#f6f6f6;border-collapse:collapse;border-color:transparent;border-spacing:0;float:none;margin:0 auto;padding:0;text-align:center;vertical-align:top;width:100%' >Dear " + customUser.UserName + "<td height = '50px' style='Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Open Sans,sans-serif;font-size:32px;font-weight:400;line-height:32px;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word'></td></tr></tbody></table><table class='m_4511308450920007087container-radius' style='border-top-width:0;border-top-color:#efefef;border-left-width:0;border-right-width:0;border-bottom-width:1px;border-bottom-color:#efefef;border-right-color:#efefef;border-left-color:#efefef;border-style:solid;border-bottom-left-radius:3px;border-bottom-right-radius:3px;display:table;padding-bottom:50px;border-spacing:60px 0;border-collapse:separate;width:100%;background:#f6f6f6;max-width:800px'><tbody><tr><td><table class='m_4511308450920007087row' style='border-collapse:collapse;border-color:transparent;border-spacing:0;display:table;padding:0;text-align:left;vertical-align:top;width:100%'><tbody><tr style = 'padding:0;text-align:left;vertical-align:top' ></ tr ></ tbody ></ table> Dear " + customUser.Name +" " + customUser.Surname + ",</p><p>Thanks for getting started with our TransX!</p><p>We need a little more information to complete your registration, including a confirmation of your email address. </p><p>Click below to confirm your email address:<br>" +
                            "<a href='https://localhost:44374/account/ConfirmEmail?userId=" + customUser.Id + "&token=" + token + "'>Confirm Email</a>" +
                        "</td></tr></tbody></table></td></tr></tbody></table>";
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
                Socials = _context.Socials.ToList(),
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
                Socials = _context.Socials.ToList(),
            };
            return View(model);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(VmProfile model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            model.Socials = _context.Socials.ToList();

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
                Socials = _context.Socials.ToList(),
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
            model.Socials = _context.Socials.ToList();

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
                        await _userManager.AddToRoleAsync(user, "Customer");

                        var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));


                        //Sending mail
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("transxmanagement@gmail.com", "TransX Managements Confirm Email");
                        mail.To.Add(user.Email);
                        mail.Body = "<table style = 'Margin:0 auto;background:#f6f6f6;border-collapse:collapse;border-color:transparent;border-spacing:0;float:none;margin:0 auto;padding:0;text-align:center;vertical-align:top;width:100%' >Dear " + user.UserName + "<td height = '50px' style='Margin:0;border-collapse:collapse!important;color:#0a0a0a;font-family:Open Sans,sans-serif;font-size:32px;font-weight:400;line-height:32px;margin:0;padding:0;text-align:left;vertical-align:top;word-wrap:break-word'></td></tr></tbody></table><table class='m_4511308450920007087container-radius' style='border-top-width:0;border-top-color:#efefef;border-left-width:0;border-right-width:0;border-bottom-width:1px;border-bottom-color:#efefef;border-right-color:#efefef;border-left-color:#efefef;border-style:solid;border-bottom-left-radius:3px;border-bottom-right-radius:3px;display:table;padding-bottom:50px;border-spacing:60px 0;border-collapse:separate;width:100%;background:#f6f6f6;max-width:800px'><tbody><tr><td><table class='m_4511308450920007087row' style='border-collapse:collapse;border-color:transparent;border-spacing:0;display:table;padding:0;text-align:left;vertical-align:top;width:100%'><tbody><tr style = 'padding:0;text-align:left;vertical-align:top' ></ tr ></ tbody ></ table> Dear " + user.UserName + ",</p><p>Thanks for getting started with our TransX!</p><p>We need a little more information to complete your registration, including a confirmation of your email address. </p><p>Click below to confirm your email address:<br>" +
                            "<a href='https://localhost:44374/account/ConfirmEmail?userId=" + user.Id + "&token=" + token + "'>Confirm Email</a>" +
                        "</td></tr></tbody></table></td></tr></tbody></table>";

                            
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
                Socials = _context.Socials.ToList(),
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(VmForgotPassword model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            model.Socials = _context.Socials.ToList();

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
                    mail.Body = "<h1>Hi Dear "+ model.Email +"</h1>" +
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
                Socials = _context.Socials.ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(VmResetPassword model)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            model.Setting = setting;
            model.Socials = _context.Socials.ToList();
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
                Socials = _context.Socials.ToList(),
            };

            if (result.Succeeded)
            {
                Notify("Confirm Email successfully");
                return View(model);
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        //Saved Blogs
        public IActionResult SavedBlogs()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();

            SavedBlogs savedBlogsDate = _context.SavedBlogs.Where(u => u.UserId == userId).OrderByDescending(d => d.AddedDate).LastOrDefault();
            if (savedBlogsDate != null)
            {
                ViewBag.LastSavedPostDate = savedBlogsDate.AddedDate;
            }


            VmProfile model = new VmProfile()
            {
                Posts = _context.Blogs.Include(c => c.Category).Include(u => u.User).Include(tp => tp.TagToBlogs).ThenInclude(t => t.Tag).Where(p => p.UserId == userId).OrderByDescending(o => o.AddedDate).ToList(),
                SavedBlogs = _context.SavedBlogs.Include(pp => pp.Blog).ThenInclude(cat => cat.Category).Include(u => u.User).Where(sa => sa.UserId == userId).OrderByDescending(o => o.AddedDate).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                User = customUsers,
                UserS = customUserS,
            };
            return View(model);
        }

        public IActionResult SavedBlogsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SavedBlogs model = _context.SavedBlogs.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            _context.SavedBlogs.Remove(model);
            _context.SaveChanges();

            Notify("Saved Blog Deleted");
            return RedirectToAction("SavedBlogs");
        }


        //My Request a Quote
        public IActionResult RequestQuote()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();


            VmProfile model = new VmProfile()
            {
                RequestQuotes = _context.RequestQuotes.Include(u=>u.User).Include(s=>s.Service).Where(us=>us.UserId == userId).OrderByDescending(a=>a.AddedDate).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                User = customUsers,
                UserS = customUserS,
            };
            return View(model);
        }

        public IActionResult RequestQuoteDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RequestQuote model = _context.RequestQuotes.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            _context.RequestQuotes.Remove(model);
            _context.SaveChanges();

            Notify("My Request a Quote Deleted");
            return RedirectToAction("RequestQuote");
        }

        //My Request a Quote
        public IActionResult Requests()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();


            VmProfile model = new VmProfile()
            {
                Requests = _context.Requests.Include(u => u.User).Include(s => s.Service).Where(us => us.UserId == userId).OrderByDescending(a => a.AddedDate).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                User = customUsers,
                UserS = customUserS,
            };
            return View(model);
        }

        public IActionResult RequestsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request model = _context.Requests.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(model);
            _context.SaveChanges();

            Notify("My Shipping Request Deleted");
            return RedirectToAction("Requests");
        }


        //My Posts
        public IActionResult Posts()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();

            Blog blogdate = _context.Blogs.Where(u => u.UserId == userId).OrderByDescending(d => d.AddedDate).LastOrDefault();
            if (blogdate != null)
            {
                ViewBag.LastPostDate = blogdate.AddedDate;
            }


            VmProfile model = new VmProfile()
            {
                Posts = _context.Blogs.Include(c=>c.Category).Include(u => u.User).Include(tp => tp.TagToBlogs).ThenInclude(t => t.Tag).Where(p => p.UserId == userId).OrderByDescending(o => o.AddedDate).ToList(),
                Tags = _context.BlogTags.Include(b => b.TagToBlogs).ThenInclude(bl => bl.Blog).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                User = customUsers,
                UserS = customUserS,
            };
            return View(model);
        }

        public IActionResult PostCreate()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();
            List<BlogCategory> categories = _context.BlogCategories.ToList();
            categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
            ViewBag.Categories = categories;

            List<BlogTag> tags = _context.BlogTags.ToList();
            ViewBag.Tags = tags;

            VmProfile model = new VmProfile()
            {
                Posts = _context.Blogs.Include(g => g.Category).Include(u => u.User).Include(tb => tb.TagToBlogs).ThenInclude(t => t.Tag).ToList(),
                Post = new Blog(),
                Categories = ViewBag.Categories,
                Tags = ViewBag.Tags,
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                User = customUsers,
                UserS=customUserS,
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult PostCreate(VmProfile model)
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();
            model.User = customUsers;
            model.UserS = customUserS;


            model.Setting = _context.Settings.FirstOrDefault();
            model.Socials = _context.Socials.ToList();
            if (ModelState.IsValid)
            {
                if (model.Post.CategoryId == 0)
                {
                    ModelState.AddModelError("CategoryId", "Categoriya secmelisiniz!");
                    List<BlogCategory> categories = _context.BlogCategories.ToList();
                    categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
                    ViewBag.Categories = categories;
                    List<BlogTag> tags = _context.BlogTags.ToList();
                    ViewBag.Tags = tags;
                    Notify("Categoriya secmelisiniz!", notificationType: NotificationType.warning);
                    return RedirectToAction("PostCreate");
                }
                if (model.Post.ImageFile != null)
                {
                    if (model.Post.ImageFile.ContentType == "image/png" || model.Post.ImageFile.ContentType == "image/jpeg" || model.Post.ImageFile.ContentType == "image/gif" || model.Post.ImageFile.ContentType == "image/svg")
                    {
                        if (model.Post.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.Post.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Blogs", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.Post.ImageFile.CopyTo(stream);
                            }

                            model.Post.Image = fileName;
                            model.Post.UserId = _userManager.GetUserId(User);
                            model.Post.AddedDate = DateTime.Now;

                            _context.Blogs.Add(model.Post);
                            _context.SaveChanges();

                            if (model.Post.TagIds == null)
                            {
                                Notify("Tag secmelisiniz!", notificationType: NotificationType.warning);
                                return RedirectToAction("PostCreate");
                            }
                            else
                            {
                                foreach (var item in model.Post.TagIds)
                                {
                                    TagToBlog tagToBlog = new TagToBlog()
                                    {
                                        BlogId = model.Post.Id,
                                        TagId = item
                                    };

                                    _context.TagToBlogs.Add(tagToBlog);
                                }
                            }


                            _context.SaveChanges();

                            Notify("Post Created");

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Notify("Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
                            return RedirectToAction("PostCreate");
                        }
                    }
                    else
                    {
                        Notify("Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
                        return RedirectToAction("PostCreate");
                    }
                }
                else
                {
                    Notify("Image Secmelisiniz!", notificationType: NotificationType.warning);
                    return RedirectToAction("PostCreate");
                }

            }

            Notify("Post Not Added!", notificationType: NotificationType.error);
            return RedirectToAction("PostCreate");
        }


        public IActionResult PostDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog blog = _context.Blogs.Include(a => a.TagToBlogs).FirstOrDefault(i => i.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            //Delete image
            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Blogs", blog.Image);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }


            //Delete tags
            foreach (var item in blog.TagToBlogs)
            {
                _context.TagToBlogs.Remove(item);
            }

            _context.Blogs.Remove(blog);
            _context.SaveChanges();

            Notify("Post Deleted");
            return RedirectToAction("posts");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
