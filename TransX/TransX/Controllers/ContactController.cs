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
    public class ContactController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public ContactController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);

            if (customUsers!=null)
            {
                ViewBag.User = customUsers.Name;
                ViewBag.Email = customUsers.Email;
            }
            
            VmBase model = new VmBase()
            {
                pageHeader = _context.PageHeaders.Where(p => p.Page == "contact").FirstOrDefault(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Message(VmBase model)
        {

            if (ModelState.IsValid)
            {
                model.Message.AddedDate = DateTime.Now;
                _context.Messages.Add(model.Message);
                _context.SaveChanges();
                Notify("Message send successfully");
            }
            else
            {
                Notify("Message not send yet!", notificationType: NotificationType.error);
            }
            return RedirectToAction("Index");
        }



        public JsonResult addSubscribe(string email)
        {
            if (email == null)
            {
                return Json(404);
            }
            bool isExist = _context.Subscribes.Any(aa => aa.Email == email);
            if (!isExist)
            {
                Subscribe subscribe = new Subscribe()
                {
                    Email = email,
                    AddedDate = DateTime.Now
                };

                _context.Subscribes.Add(subscribe);
                _context.SaveChanges();
            }
            else
            {
                return Json(411);
            }
            

            return Json(200);
        }


        public JsonResult addMessage(string email, string name, string subject, string content)
        {
            if (email == null && name ==null && subject ==null && content==null)
            {
                return Json(404);
            }

            Message message = new Message()
            {
                Email = email,
                Name = name,
                Subject = subject,
                Content = content,
                AddedDate = DateTime.Now
            };

            _context.Messages.Add(message);
            _context.SaveChanges();




            return Json(200);
        }

    }
}
