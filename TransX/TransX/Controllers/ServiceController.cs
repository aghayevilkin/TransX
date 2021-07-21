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
    public class ServiceController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public ServiceController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            VmService model = new VmService()
            {
                Services = _context.Services.Include(g => g.Category).Include(u => u.User).Include(be => be.BenefitsToServices).ThenInclude(ben => ben.Benefit)
                .Include(so => so.ServiceOfferedToServices).ThenInclude(sos => sos.ServiceOffered)
                .Include(iss => iss.IndustriesServedToServices).ThenInclude(siss => siss.IndustriesServed).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "service").FirstOrDefault(),
            };
            return View(model);
        }
    }
}
