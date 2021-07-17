using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;
using TransX.ViewModels;

namespace TransX.Controllers
{
    public class AboutController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AboutController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.Page = "about";
            VmAbout model = new VmAbout()
            {
                Setting = _context.Settings.FirstOrDefault(),
                About = _context.Abouts.FirstOrDefault(),
                AboutMission = _context.AboutMissions.FirstOrDefault(),
                AboutServices = _context.AboutServices.ToList(),
                Achievements = _context.Achievements.ToList(),
                Histories = _context.Histories.ToList(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "about").FirstOrDefault(),
            };
            return View(model);
        }
    }
}
