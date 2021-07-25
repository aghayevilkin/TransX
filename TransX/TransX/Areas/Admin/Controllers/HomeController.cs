using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Controllers;
using TransX.Data;
using TransX.Models;
using TransX.ViewModels;

namespace TransX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    public class HomeController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }


        //Request a Quote
        public IActionResult RequestQuote()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();


            List<RequestQuote> model = _context.RequestQuotes.Include(u => u.User).Include(s => s.Service).Where(us => us.UserId == userId).OrderByDescending(a => a.AddedDate).ToList();

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

            Notify("Request a Quote Deleted");
            return RedirectToAction("RequestQuote");
        }

    }
}
