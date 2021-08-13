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
            VmBase model = new VmBase()
            {
                pageHeader = _context.PageHeaders.Where(p => p.Page == "home").FirstOrDefault(),
                HomeAbout = _context.HomeAbouts.FirstOrDefault(),
                HomeImage = _context.HomeImages.FirstOrDefault(),
                CaseStudies = _context.CaseStudies.OrderByDescending(cc => cc.Id).ToList(),
                Blogs = _context.Blogs.ToList(),
                Services = _context.Services.ToList(),
                CustomUsers = _context.CustomUsers.ToList(),
                RequestQuotes = _context.RequestQuotes.ToList(),
                Requests = _context.Requests.ToList(),
                Subscribes = _context.Subscribes.ToList(),
                Messages = _context.Messages.ToList(),
                Partners = _context.Partners.ToList(),
            };
            return View(model);
        }


        public IActionResult CreateImage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateImage(HomeImage model)
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
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Home", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.HomeImages.Add(model);
                            _context.SaveChanges();

                            Notify("Home Image Created");
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
            HomeImage model = _context.HomeImages.Find(id);
            if (model == null)
            {
                return NotFound();
            }


            HomeImage homeImage = _context.HomeImages.FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateImage(HomeImage model)
        {

            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {

                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Home", model.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Home", fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Entry(model).State = EntityState.Modified;
                            Notify("Home Images Updated");
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
                    Notify("Home Image Updated");

                    return RedirectToAction("Index");
                }

            }
            return View(model);
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


            PageHeader pageHeader = _context.PageHeaders.Where(p => p.Page == "home").FirstOrDefault();

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


        //HomeAbout
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeAbout model)
        {
            if (ModelState.IsValid)
            {
                _context.HomeAbouts.Add(model);
                _context.SaveChanges();
                Notify("Home About Created");

                return RedirectToAction("Index");

            }
            return View(model);
        }


        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            HomeAbout model = _context.HomeAbouts.Find(id);
            if (model == null)
            {
                return NotFound();
            }


            HomeAbout about = _context.HomeAbouts.FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(HomeAbout model)
        {

            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                Notify("Home About Updated");
                _context.SaveChanges();


                return RedirectToAction("Index");

            }
            return View(model);
        }



        //CaseStudies
        public IActionResult CreateCaseStudies()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCaseStudies(CaseStudies model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 10097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Home", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.CaseStudies.Add(model);
                            _context.SaveChanges();

                            Notify("Case Studies Image Created");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Notify("Siz maksimum 10 Mb hecmde fayllari upload ede bilersiniz!", notificationType: NotificationType.warning);
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

        public IActionResult DeleteCaseStudies(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CaseStudies model = _context.CaseStudies.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            //Delete image
            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Home", model.Image);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }



            _context.CaseStudies.Remove(model);
            _context.SaveChanges();
            Notify("Case Studies Image Deleted");

            return RedirectToAction("Index");
        }

        //Request a Quote
        public IActionResult RequestQuote()
        {
            string userId = _userManager.GetUserId(User);
            CustomUser customUsers = _context.CustomUsers.Find(userId);
            List<CustomUser> customUserS = _context.CustomUsers.Include(u => u.SocialToUsers).ThenInclude(sc => sc.Social).Where(aa => aa.SocialToUsers.Any(bb => bb.User.Id == userId)).ToList();


            List<RequestQuote> model = _context.RequestQuotes.Include(u => u.User).Include(s => s.Service).OrderByDescending(a => a.AddedDate).ToList();

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
