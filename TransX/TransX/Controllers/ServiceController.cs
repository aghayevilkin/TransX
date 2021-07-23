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
            ViewBag.Page = "service";
            VmService model = new VmService()
            {
                Services = _context.Services.Include(g => g.Category).Include(u => u.User).Include(be => be.BenefitsToServices).ThenInclude(ben => ben.Benefit)
                .Include(so => so.ServiceOfferedToServices).ThenInclude(sos => sos.ServiceOffered)
                .Include(iss => iss.IndustriesServedToServices).ThenInclude(siss => siss.IndustriesServed).ToList(),
                TransporteriumServices = _context.TransporteriumServices.ToList(),
                ServiceStepsforWorks = _context.ServiceStepsforWorks.ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "service").FirstOrDefault(),
            };
            return View(model);
        }


        public IActionResult Details(int id)
        {
            ViewBag.Page = "service";
            int catId = _context.Services.Find(id).CategoryId;
            var userIdd = _userManager.GetUserId(User);
            ViewBag.categoryId = catId;
            VmService model = new VmService()
            {
                Service = _context.Services.Include(g => g.Category).Include(u => u.User).Include(be => be.BenefitsToServices).ThenInclude(ben => ben.Benefit)
                .Include(so => so.ServiceOfferedToServices).ThenInclude(sos => sos.ServiceOffered)
                .Include(iss => iss.IndustriesServedToServices).ThenInclude(siss => siss.IndustriesServed).FirstOrDefault(b => b.Id == id),

                Services = _context.Services.Include(g => g.Category).Include(u => u.User).Include(be => be.BenefitsToServices).ThenInclude(ben => ben.Benefit)
                .Include(so => so.ServiceOfferedToServices).ThenInclude(sos => sos.ServiceOffered)
                .Include(iss => iss.IndustriesServedToServices).ThenInclude(siss => siss.IndustriesServed).Where(aa => aa.User.SocialToUsers.Any(bb => bb.User.Id == userIdd)).ToList(),

                OtherServices = _context.Services.ToList(),
                Categories = _context.ServiceCategories.Include(b => b.Services).ToList(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "servicedetails").FirstOrDefault(),
                pageHeaderDetails = _context.PageHeaders.Where(p => p.Page == "servicedetails").FirstOrDefault(),               
                Benefits = _context.Benefits.Include(tb => tb.BenefitsToServices).ThenInclude(b => b.Benefit).Where(t => t.BenefitsToServices.Any(tbb => tbb.ServiceId == id)).ToList(),
                ServiceOffereds = _context.ServiceOffereds.Include(tb => tb.ServiceOfferedToServices).ThenInclude(b => b.Service).Where(t => t.ServiceOfferedToServices.Any(tbb => tbb.ServiceId == id)).ToList(),
                IndustriesServeds = _context.IndustriesServeds.Include(tb => tb.IndustriesServedToServices).ThenInclude(b => b.Service).Where(t => t.IndustriesServedToServices.Any(tbb => tbb.ServiceId == id)).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
            };


            return View(model);
        }

    }
}
