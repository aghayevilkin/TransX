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
using TransX.ViewModels;

namespace TransX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    public class RequestController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public RequestController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            VmRequest model = new VmRequest()
            {
                pageHeader = _context.PageHeaders.Where(p => p.Page == "request").FirstOrDefault(),
                Requests = _context.Requests.Include(s=>s.Service).Include(u=>u.User).OrderByDescending(i => i.AddedDate).ToList(),
            };
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                Notify("Id must be empty", notificationType: NotificationType.error);

                return RedirectToAction("index");
            }

            Request model = _context.Requests.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }


            _context.Requests.Remove(model);
            _context.SaveChanges();
            Notify("Request Deleted");

            return RedirectToAction("index");
        }

        //PageHeader
        public IActionResult CreateHeader()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateHeader(PageHeader model)
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
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/PageHeader", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.PageHeaders.Add(model);
                            _context.SaveChanges();
                            Notify("Page Header Created");

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
                    Notify("No Image Found!", notificationType: NotificationType.warning);
                    ModelState.AddModelError("ImageFile", "No Image Found!");
                }

            }
            return View(model);
        }


        public IActionResult UpdateHeader(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PageHeader Header = _context.PageHeaders.Find(id);
            if (Header == null)
            {
                return NotFound();
            }


            PageHeader pageHeader = _context.PageHeaders.Where(p => p.Page == "request").FirstOrDefault();

            return View(Header);
        }

        [HttpPost]
        public IActionResult UpdateHeader(PageHeader model)
        {

            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {

                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/PageHeader", model.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/PageHeader", fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Entry(model).State = EntityState.Modified;
                            Notify("Page Header Updated");
                            _context.SaveChanges();


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

                    _context.SaveChanges();
                    Notify("Page Header Updated");

                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }
    }
}
