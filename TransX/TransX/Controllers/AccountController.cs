using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
                User = customUsers,
                UserS = customUserS,
            };
            return View(model);
        }



        public IActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(VmBase model)
        {
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



        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(VmBase model)
        {

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



        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
