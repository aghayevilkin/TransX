using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;
using TransX.Models;
using TransX.ViewModels;

namespace TransX.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }


        public IActionResult Index()
        {
            List<Service> services = _context.Services.ToList();
            services.Insert(0, new Service() { Id = 0, Title = "Choose transport" });
            ViewBag.Services = services;
            ViewBag.Page = "home";
            VmBase model = new VmBase()
            {
                Setting = _context.Settings.FirstOrDefault(),
            };
            return View(model);
        }



        public JsonResult addRequestQuote(string from, string to, DateTime when, int service)
        {
            var userId = _userManager.GetUserId(User);
            if (from == null && to == null && when == null && service == 0)
            {
                return Json(404);
            }

            RequestQuote model = new RequestQuote()
            {
                From = from,
                To = to,
                When = when,
                ServiceId = service,
                UserId = userId,
                AddedDate = DateTime.Now,
            };

            _context.RequestQuotes.Add(model);
            _context.SaveChanges();




            return Json(200);
        }


    }
}
