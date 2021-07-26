using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;
using TransX.ViewModels;

namespace TransX.Controllers
{
    public class TeamController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TeamController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.Page = "team";
            VmTeam model = new VmTeam()
            {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "team").FirstOrDefault(),
                TeamImage = _context.TeamImages.FirstOrDefault(),
                CustomUser =_context.CustomUsers.Include(sc=>sc.SocialToUsers).ThenInclude(s=>s.Social).Where(aa => aa.IsTeam==true).ToList(),
                AboutServices = _context.AboutServices.Take(4).ToList(),
            };
            return View(model);
        }
    }
}
