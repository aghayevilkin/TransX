using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;
using TransX.Models;
using TransX.ViewModels;

namespace TransX.Controllers
{
    public class RequestController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public RequestController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            List<Service> services = _context.Services.ToList();
            services.Insert(0, new Service() { Id = 0, Title = "Choose transport" });
            ViewBag.Services = services;

            List<City> cities = _context.Cities.ToList();
            cities.Insert(0, new City() { Id = 0, Name = "Delivery city" });
            ViewBag.Cities = cities;
            
            List<City> citiestwo = _context.Cities.ToList();
            citiestwo.Insert(0, new City() { Id = 0, Name = "City of departure" });
            ViewBag.CitiesTwo = citiestwo;

            ViewBag.Page = "request";
            VmRequest model = new VmRequest()
            {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "request").FirstOrDefault(),
                CustomUser = _context.CustomUsers.FirstOrDefault(u => u.Id == userId),
            };
            return View(model);
        }



        public JsonResult addRequest(string name, string email, int service, int cities, int citiestwo, int weight, int height, int width, int length, string InsuranceOrPackaging)
        {
            var userId = _userManager.GetUserId(User);
            if (name == null && email == null && service == 0)
            {
                return Json(404);
            }

            var delivery = _context.Cities.Where(c => c.Id == cities).FirstOrDefault();
            var departure = _context.Cities.Where(c => c.Id == citiestwo).FirstOrDefault();

            if (userId==null)
            {
                Request model = new Request()
                {
                    Name = name,
                    Email = email,
                    ServiceId = service,
                    DeliveryCity = delivery.Name,
                    DepartureCity = departure.Name,
                    Weight = weight,
                    Height = height,
                    Widhth = width,
                    Lenght = length,
                    InsuranceOrPackaging = InsuranceOrPackaging,
                    AddedDate = DateTime.Now,
                };

                _context.Requests.Add(model);
                _context.SaveChanges();
            }
            else
            {
                Request model = new Request()
                {
                    Name = name,
                    Email = email,
                    ServiceId = service,
                    DeliveryCity = delivery.Name,
                    DepartureCity = departure.Name,
                    Weight = weight,
                    Height = height,
                    Widhth = width,
                    Lenght = length,
                    InsuranceOrPackaging = InsuranceOrPackaging,
                    UserId = userId,
                    AddedDate = DateTime.Now,
                };

                _context.Requests.Add(model);
                _context.SaveChanges();
            }
            

            return Json(200);
        }

    }
}
