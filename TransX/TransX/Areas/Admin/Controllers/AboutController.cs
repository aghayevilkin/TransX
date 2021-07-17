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
    public class AboutController : BaseController
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
            VmAbout model = new VmAbout()
            {
                pageHeader = _context.PageHeaders.Where(p => p.Page == "about").FirstOrDefault(),
                About = _context.Abouts.FirstOrDefault(),
                AboutMission = _context.AboutMissions.FirstOrDefault(),
                AboutServices =_context.AboutServices.ToList(),
                Achievements =_context.Achievements.ToList(),
                Histories =_context.Histories.ToList(),
            };
            return View(model);
        }


        //AboutServices
        public IActionResult CreateAboutServices()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAboutServices(AboutServices model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title != null)
                {
                    if (model.Icon != null)
                    {
                        _context.AboutServices.Add(model);
                        _context.SaveChanges();
                        Notify("About Services Created");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Notify("About Services Not Created", notificationType: NotificationType.error);
                        ModelState.AddModelError("Icon", "Icon must be empty");
                    }
                    
                }
                else
                {
                    Notify("About Services Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Title", "Title must be empty");
                }


            }
            return View(model);
        }

        public IActionResult UpdateAboutServices(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AboutServices aboutServices = _context.AboutServices.FirstOrDefault(i => i.Id == id);
            if (aboutServices == null)
            {
                return NotFound();
            }
            return View(aboutServices);
        }


        [HttpPost]
        public IActionResult UpdateAboutServices(AboutServices model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title != null)
                {
                    if (model.Icon != null)
                    {
                        _context.Entry(model).State = EntityState.Modified;
                        _context.SaveChanges();
                        Notify("About Services Update");

                        return RedirectToAction("index");
                    }
                    else
                    {
                        Notify("About Services Not Created", notificationType: NotificationType.error);
                        ModelState.AddModelError("Icon", "Icon must be empty");
                    }
                    
                }
                else
                {
                    Notify("About Services Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Title", "Title must be empty");
                }


            }
            return View(model);
        }

        public IActionResult DeleteAboutServices(int? id)
        {
            if (id == null)
            {
                Notify("Id must be empty", notificationType: NotificationType.error);

                return RedirectToAction("index");
            }

            AboutServices aboutServices = _context.AboutServices.FirstOrDefault(i => i.Id == id);
            if (aboutServices == null)
            {
                return NotFound();
            }


            _context.AboutServices.Remove(aboutServices);
            _context.SaveChanges();
            Notify("About Services Deleted");

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


            PageHeader pageHeader = _context.PageHeaders.Where(p => p.Page == "about").FirstOrDefault();

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

        //Achievement
        public IActionResult CreateAchievement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAchievement(Achievement model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title != null)
                {
                    if (model.CountNum != 0)
                    {
                        _context.Achievements.Add(model);
                        _context.SaveChanges();
                        Notify("About Achievement Created");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Notify("Achievement Not Created", notificationType: NotificationType.error);
                        ModelState.AddModelError("CountNum", "Count Number must be empty");
                    }

                }
                else
                {
                    Notify("Achievement Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Title", "Title must be empty");
                }


            }
            return View(model);
        }

        public IActionResult UpdateAchievement(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Achievement achievement = _context.Achievements.FirstOrDefault(i => i.Id == id);
            if (achievement == null)
            {
                return NotFound();
            }
            return View(achievement);
        }


        [HttpPost]
        public IActionResult UpdateAchievement(Achievement model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title != null)
                {
                    if (model.CountNum != 0)
                    {
                        _context.Entry(model).State = EntityState.Modified;
                        _context.SaveChanges();
                        Notify("About Achievement Updated");

                        return RedirectToAction("index");
                    }
                    else
                    {
                        Notify("Achievement Not Created", notificationType: NotificationType.error);
                        ModelState.AddModelError("CountNum", "Count Number must be empty");
                    }

                }
                else
                {
                    Notify("Achievement Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Title", "Title must be empty");
                }


            }
            return View(model);
        }

        public IActionResult DeleteAchievement(int? id)
        {
            if (id == null)
            {
                Notify("Id must be empty", notificationType: NotificationType.error);

                return RedirectToAction("index");
            }

            Achievement achievement = _context.Achievements.FirstOrDefault(i => i.Id == id);
            if (achievement == null)
            {
                return NotFound();
            }


            _context.Achievements.Remove(achievement);
            _context.SaveChanges();
            Notify("About Achievement Deleted");

            return RedirectToAction("index");
        }


        //History
        public IActionResult CreateHistory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateHistory(History model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title != null)
                {
                    if (model.Year != 0)
                    {
                        _context.Histories.Add(model);
                        _context.SaveChanges();
                        Notify("About Histories Created");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Notify("Histories Not Created", notificationType: NotificationType.error);
                        ModelState.AddModelError("Year", "Count Number must be empty");
                    }

                }
                else
                {
                    Notify("Histories Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Title", "Title must be empty");
                }


            }
            return View(model);
        }

        public IActionResult UpdateHistory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            History history = _context.Histories.FirstOrDefault(i => i.Id == id);
            if (history == null)
            {
                return NotFound();
            }
            return View(history);
        }


        [HttpPost]
        public IActionResult UpdateHistory(History model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title != null)
                {
                    if (model.Year != 0)
                    {
                        _context.Entry(model).State = EntityState.Modified;
                        _context.SaveChanges();
                        Notify("About Histories Updated");

                        return RedirectToAction("index");
                    }
                    else
                    {
                        Notify("Histories Not Created", notificationType: NotificationType.error);
                        ModelState.AddModelError("Year", "Count Number must be empty");
                    }

                }
                else
                {
                    Notify("Histories Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Title", "Title must be empty");
                }


            }
            return View(model);
        }

        public IActionResult DeleteHistory(int? id)
        {
            if (id == null)
            {
                Notify("Id must be empty", notificationType: NotificationType.error);

                return RedirectToAction("index");
            }

            History history = _context.Histories.FirstOrDefault(i => i.Id == id);
            if (history == null)
            {
                return NotFound();
            }


            _context.Histories.Remove(history);
            _context.SaveChanges();
            Notify("About Histories Deleted");

            return RedirectToAction("index");
        }



        //Mission
        public IActionResult CreateMission()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMission(AboutMission model)
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
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Abouts", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.AboutMissions.Add(model);
                            _context.SaveChanges();
                            Notify("About Mission Created");

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


        public IActionResult UpdateMission(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            AboutMission model = _context.AboutMissions.Find(id);
            if (model == null)
            {
                return NotFound();
            }


            AboutMission aboutMission = _context.AboutMissions.FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateMission(AboutMission model)
        {

            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {

                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Abouts", model.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Abouts", fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Entry(model).State = EntityState.Modified;
                            Notify("About Mission Updated");
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
                    Notify("About Mission Updated");

                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }



        //About
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(About model)
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
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Abouts", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Abouts.Add(model);
                            _context.SaveChanges();
                            Notify("About Created");

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


        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            About model = _context.Abouts.Find(id);
            if (model == null)
            {
                return NotFound();
            }


            About about = _context.Abouts.FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(About model)
        {

            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {

                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Abouts", model.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Abouts", fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Entry(model).State = EntityState.Modified;
                            Notify("About Updated");
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
                    Notify("About Updated");

                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }

    }
}
