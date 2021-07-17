using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransX.Controllers;
using TransX.Data;
using TransX.Models;

namespace TransX.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    public class TestimonialController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TestimonialController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            Testimonials test = _context.Testimonials.FirstOrDefault();
            ViewBag.Testimonial = test.Image;
            ViewBag.TestimonialId = test.Id;

            List<Testimonials> testimonials = _context.Testimonials.Include(u=>u.User).ToList();
            return View(testimonials);
        }

        public IActionResult CreateImage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateImage(Testimonials model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Testimonials", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;
                            model.AddedDate = DateTime.Now;

                            _context.Testimonials.Add(model);
                            _context.SaveChanges();

                            Notify("Testimonials Created");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Notify("Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
                            ModelState.AddModelError("ImageFile", "Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!");
                        }
                    }
                    else
                    {
                        Notify("Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
                        ModelState.AddModelError("ImageFile", "Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!");
                    }
                }
                else
                {
                    Notify("No Image Found!", notificationType: NotificationType.error);
                    ModelState.AddModelError("ImageFile", "No Image Found!");
                }

            }

            return View(model);
        }




        public IActionResult UpdateImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Testimonials testimonials = _context.Testimonials.FirstOrDefault(i => i.Id == id);
            if (testimonials == null)
            {
                return NotFound();
            }


            return View(testimonials);
        }



        [HttpPost]
        public IActionResult UpdateImage(Testimonials model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Testimonials", model.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Testimonials", fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Entry(model).State = EntityState.Modified;
                            _context.Entry(model).Property(a => a.AddedDate).IsModified = false;

                            _context.SaveChanges();
                            Notify("Testimonials Image Updated");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Notify("Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
                            ModelState.AddModelError("ImageFile", "Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!");
                        }
                    }
                    else
                    {
                        Notify("Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
                        ModelState.AddModelError("ImageFile", "Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!");
                    }
                }
                else
                {

                    _context.Entry(model).State = EntityState.Modified;
                    _context.Entry(model).Property(a => a.AddedDate).IsModified = false;

                    _context.SaveChanges();

                    Notify("Image Not Update!", notificationType: NotificationType.warning);
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Testimonials model)
        {
            if (ModelState.IsValid)
            {
                if (model.Content!=null)
                {
                    model.UserId = _userManager.GetUserId(User);
                    model.AddedDate = DateTime.Now;
                    _context.Testimonials.Add(model);
                    _context.SaveChanges();
                    Notify("Testimonials Created");
                    return RedirectToAction("Index");
                }
                else
                {
                    Notify("Testimonials Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Content", "Content must be empty");
                }
                

            }
            return View(model);
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Testimonials testimonials = _context.Testimonials.FirstOrDefault(i => i.Id == id);
            if (testimonials == null)
            {
                return NotFound();
            }
            return View(testimonials);
        }


        [HttpPost]
        public IActionResult Update(Testimonials model)
        {
            if (ModelState.IsValid)
            {
                if (model.Content != null)
                {
                    _context.Entry(model).State = EntityState.Modified;
                    _context.Entry(model).Property(a => a.UserId).IsModified = false;
                    _context.Entry(model).Property(a => a.AddedDate).IsModified = false;
                    _context.SaveChanges();
                    Notify("Testimonials Updated");

                    return RedirectToAction("index");
                }
                else
                {
                    Notify("Testimonials Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Content", "Content must be empty");
                }
                

            }
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Testimonials testimonials = _context.Testimonials.FirstOrDefault(i => i.Id == id);
            if (testimonials == null)
            {
                return NotFound();
            }


            _context.Testimonials.Remove(testimonials);
            _context.SaveChanges();
            Notify("Testimonials Deleted");

            return RedirectToAction("index");
        }


    }
}
