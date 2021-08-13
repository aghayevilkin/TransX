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
    public class ServiceController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ServiceController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            List<ServiceCategory> categories = _context.ServiceCategories.ToList();
            categories.Insert(0, new ServiceCategory() { Id = 0, Name = "Select" });
            ViewBag.Categories = categories;

            List<Benefit> benefits = _context.Benefits.ToList();
            ViewBag.Benefits = benefits;

            List<ServiceOffered> serviceOffereds = _context.ServiceOffereds.ToList();
            ViewBag.ServiceOffereds = serviceOffereds;
            
            List<IndustriesServed> industriesServeds = _context.IndustriesServeds.ToList();
            ViewBag.IndustriesServeds = industriesServeds;

            VmService model = new VmService()
            {
                Services = _context.Services.Include(g => g.Category).Include(u => u.User).Include(be => be.BenefitsToServices).ThenInclude(ben=>ben.Benefit)
                .Include(so=>so.ServiceOfferedToServices).ThenInclude(sos=>sos.ServiceOffered)
                .Include(iss=>iss.IndustriesServedToServices).ThenInclude(siss=>siss.IndustriesServed).OrderByDescending(added=>added.AddedDate).ToList(),
                Service = new Service(),
                Categories = ViewBag.Categories,
                Benefits = ViewBag.Benefits,
                ServiceOffereds = ViewBag.ServiceOffereds,
                IndustriesServeds = ViewBag.IndustriesServeds,
                pageHeader = _context.PageHeaders.Where(p => p.Page == "service").FirstOrDefault(),
                pageHeaderDetails = _context.PageHeaders.Where(p => p.Page == "servicedetails").FirstOrDefault(),
            };
            return View(model);
        }



        //Create
        public IActionResult Create()
        {
            List<ServiceCategory> categories = _context.ServiceCategories.ToList();
            categories.Insert(0, new ServiceCategory() { Id = 0, Name = "Select" });
            ViewBag.Categories = categories;

            List<Benefit> benefits = _context.Benefits.ToList();
            ViewBag.Benefits = benefits;

            List<ServiceOffered> serviceOffereds = _context.ServiceOffereds.ToList();
            ViewBag.ServiceOffereds = serviceOffereds;

            List<IndustriesServed> industriesServeds = _context.IndustriesServeds.ToList();
            ViewBag.IndustriesServeds = industriesServeds;

            Service model = new Service();

            return View(model);
        }


        [HttpPost]
        public IActionResult Create(Service model)
        {
            if (ModelState.IsValid)
            {
                if (model.CategoryId != 0)
                {
                    if (model.ImageFile != null)
                    {
                        if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                        {
                            if (model.ImageFile.Length <= 2097152)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Services", fileName);
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    model.ImageFile.CopyTo(stream);
                                }

                                model.Image = fileName;
                                model.UserId = _userManager.GetUserId(User);
                                model.AddedDate = DateTime.Now;

                                _context.Services.Add(model);
                                _context.SaveChanges();

                                if (model.BenefitIds != null)
                                {
                                    foreach (var item in model.BenefitIds)
                                    {
                                        BenefitsToService benefitsToService = new BenefitsToService()
                                        {
                                            ServiceId = model.Id,
                                            BenefitId = item
                                        };

                                        _context.BenefitsToServices.Add(benefitsToService);
                                    }
                                }

                                if (model.ServiceOfferedIds != null)
                                {
                                    foreach (var item in model.ServiceOfferedIds)
                                    {
                                        ServiceOfferedToService serviceOfferedToService = new ServiceOfferedToService()
                                        {
                                            ServiceId = model.Id,
                                            ServiceOfferedId = item
                                        };

                                        _context.ServiceOfferedToServices.Add(serviceOfferedToService);
                                    }
                                }

                                if (model.IndustriesServedIds != null)
                                {
                                    foreach (var item in model.IndustriesServedIds)
                                    {
                                        IndustriesServedToService industriesServedToService = new IndustriesServedToService()
                                        {
                                            ServiceId = model.Id,
                                            IndustriesServedId = item
                                        };

                                        _context.IndustriesServedToServices.Add(industriesServedToService);
                                    }
                                }


                                _context.SaveChanges();
                                Notify("Service Created");

                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ModelState.AddModelError("ImageFile", "Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("ImageFile", "Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!");
                        }
                    }
                    else
                    {
                        Notify("No Image Found!", notificationType: NotificationType.warning);
                        ModelState.AddModelError("ImageFile", "No Image Found!");
                    }
                }
                else
                {
                    ModelState.AddModelError("CategoryId", "Categoriya secmelisiniz!");
                    List<BlogCategory> categories = _context.BlogCategories.ToList();
                    categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
                    ViewBag.Categories = categories;
                    Notify("Categoriya secmelisiniz!", notificationType: NotificationType.warning);
                    return RedirectToAction("create");
                }



            }

            Notify("Service Not Added!", notificationType: NotificationType.error);
            return RedirectToAction("create");
        }



        //Update
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service service = _context.Services.Include(g => g.Category).Include(u => u.User).Include(be => be.BenefitsToServices).ThenInclude(ben => ben.Benefit)
                .Include(so => so.ServiceOfferedToServices).ThenInclude(sos => sos.ServiceOffered)
                .Include(iss => iss.IndustriesServedToServices).ThenInclude(siss => siss.IndustriesServed).FirstOrDefault(i => i.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            List<ServiceCategory> categories = _context.ServiceCategories.ToList();
            categories.Insert(0, new ServiceCategory() { Id = 0, Name = "Select" });
            ViewBag.Categories = categories;

            List<Benefit> benefits = _context.Benefits.ToList();
            ViewBag.Benefits = benefits;

            List<ServiceOffered> serviceOffereds = _context.ServiceOffereds.ToList();
            ViewBag.ServiceOffereds = serviceOffereds;

            List<IndustriesServed> industriesServeds = _context.IndustriesServeds.ToList();
            ViewBag.IndustriesServeds = industriesServeds;

            List<int> benefitIds = new List<int>();

            foreach (var item in service.BenefitsToServices)
            {
                benefitIds.Add(item.BenefitId);
            }

            service.BenefitIds = benefitIds.ToArray();

            List<int> serviceOfferedIds = new List<int>();

            foreach (var item in service.ServiceOfferedToServices)
            {
                serviceOfferedIds.Add(item.ServiceOfferedId);
            }

            service.ServiceOfferedIds = serviceOfferedIds.ToArray();

            List<int> industriesServedIds = new List<int>();

            foreach (var item in service.IndustriesServedToServices)
            {
                industriesServedIds.Add(item.IndustriesServedId);
            }

            service.IndustriesServedIds = industriesServedIds.ToArray();

            return View(service);
        }



        [HttpPost]
        public IActionResult Update(Service model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.CategoryId == 0)
                    {
                        ModelState.AddModelError("CategoryId", "Categoriya secmelisiniz!");
                        List<BlogCategory> categories = _context.BlogCategories.ToList();
                        categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
                        ViewBag.Categories = categories;
                        return View(model);
                    }
                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Services", model.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Services", fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Entry(model).State = EntityState.Modified;
                            _context.Entry(model).Property(a => a.UserId).IsModified = false;
                            _context.Entry(model).Property(a => a.AddedDate).IsModified = false;

                            _context.SaveChanges();


                            foreach (var item in _context.Services.Include(a => a.BenefitsToServices).FirstOrDefault(i => i.Id == model.Id).BenefitsToServices)
                            {
                                _context.BenefitsToServices.Remove(item);
                            }
                            _context.SaveChanges();
                            
                            foreach (var item in _context.Services.Include(a => a.ServiceOfferedToServices).FirstOrDefault(i => i.Id == model.Id).ServiceOfferedToServices)
                            {
                                _context.ServiceOfferedToServices.Remove(item);
                            }
                            _context.SaveChanges();
                            
                            foreach (var item in _context.Services.Include(a => a.IndustriesServedToServices).FirstOrDefault(i => i.Id == model.Id).IndustriesServedToServices)
                            {
                                _context.IndustriesServedToServices.Remove(item);
                            }
                            _context.SaveChanges();


                            if (model.BenefitIds != null)
                            {
                                foreach (var item in model.BenefitIds)
                                {
                                    BenefitsToService benefitsToService = new BenefitsToService()
                                    {
                                        ServiceId = model.Id,
                                        BenefitId = item
                                    };

                                    _context.BenefitsToServices.Add(benefitsToService);
                                }
                            }

                            if (model.ServiceOfferedIds != null)
                            {
                                foreach (var item in model.ServiceOfferedIds)
                                {
                                    ServiceOfferedToService serviceOfferedToService = new ServiceOfferedToService()
                                    {
                                        ServiceId = model.Id,
                                        ServiceOfferedId = item
                                    };

                                    _context.ServiceOfferedToServices.Add(serviceOfferedToService);
                                }
                            }

                            if (model.IndustriesServedIds != null)
                            {
                                foreach (var item in model.IndustriesServedIds)
                                {
                                    IndustriesServedToService industriesServedToService = new IndustriesServedToService()
                                    {
                                        ServiceId = model.Id,
                                        IndustriesServedId = item
                                    };

                                    _context.IndustriesServedToServices.Add(industriesServedToService);
                                }
                            }
                            _context.SaveChanges();

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("ImageFile", "Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!");
                    }
                }
                else
                {
                    if (model.CategoryId == 0)
                    {
                        ModelState.AddModelError("CategoryId", "Categoriya secmelisiniz!");
                        List<BlogCategory> categories = _context.BlogCategories.ToList();
                        categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
                        ViewBag.Categories = categories;
                        return View(model);
                    }

                    _context.Entry(model).State = EntityState.Modified;
                    _context.Entry(model).Property(a => a.UserId).IsModified = false;
                    _context.Entry(model).Property(a => a.AddedDate).IsModified = false;

                    _context.SaveChanges();


                    foreach (var item in _context.Services.Include(a => a.BenefitsToServices).FirstOrDefault(i => i.Id == model.Id).BenefitsToServices)
                    {
                        _context.BenefitsToServices.Remove(item);
                    }
                    _context.SaveChanges();

                    foreach (var item in _context.Services.Include(a => a.ServiceOfferedToServices).FirstOrDefault(i => i.Id == model.Id).ServiceOfferedToServices)
                    {
                        _context.ServiceOfferedToServices.Remove(item);
                    }
                    _context.SaveChanges();

                    foreach (var item in _context.Services.Include(a => a.IndustriesServedToServices).FirstOrDefault(i => i.Id == model.Id).IndustriesServedToServices)
                    {
                        _context.IndustriesServedToServices.Remove(item);
                    }
                    _context.SaveChanges();


                    if (model.BenefitIds != null)
                    {
                        foreach (var item in model.BenefitIds)
                        {
                            BenefitsToService benefitsToService = new BenefitsToService()
                            {
                                ServiceId = model.Id,
                                BenefitId = item
                            };

                            _context.BenefitsToServices.Add(benefitsToService);
                        }
                    }

                    if (model.ServiceOfferedIds != null)
                    {
                        foreach (var item in model.ServiceOfferedIds)
                        {
                            ServiceOfferedToService serviceOfferedToService = new ServiceOfferedToService()
                            {
                                ServiceId = model.Id,
                                ServiceOfferedId = item
                            };

                            _context.ServiceOfferedToServices.Add(serviceOfferedToService);
                        }
                    }

                    if (model.IndustriesServedIds != null)
                    {
                        foreach (var item in model.IndustriesServedIds)
                        {
                            IndustriesServedToService industriesServedToService = new IndustriesServedToService()
                            {
                                ServiceId = model.Id,
                                IndustriesServedId = item
                            };

                            _context.IndustriesServedToServices.Add(industriesServedToService);
                        }
                    }
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }



        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service service = _context.Services.Include(be => be.BenefitsToServices).ThenInclude(ben => ben.Benefit)
                .Include(so => so.ServiceOfferedToServices).ThenInclude(sos => sos.ServiceOffered)
                .Include(iss => iss.IndustriesServedToServices).ThenInclude(siss => siss.IndustriesServed).FirstOrDefault(i => i.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            //Delete image
            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Services", service.Image);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }


            //Delete Benfits
            foreach (var item in service.BenefitsToServices)
            {
                _context.BenefitsToServices.Remove(item);
            }

            //Delete Service Offered
            foreach (var item in service.ServiceOfferedToServices)
            {
                _context.ServiceOfferedToServices.Remove(item);
            }

            //Delete Industries Served
            foreach (var item in service.IndustriesServedToServices)
            {
                _context.IndustriesServedToServices.Remove(item);
            }

            _context.Services.Remove(service);
            _context.SaveChanges();
            Notify("Service Deleted");
            return RedirectToAction("Index");
        }

        //ServiceCategory
        public IActionResult Category()
        {
            List<ServiceCategory> serviceCategories = _context.ServiceCategories.ToList();
            return View(serviceCategories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(ServiceCategory model)
        {
            if (ModelState.IsValid)
            {

                _context.ServiceCategories.Add(model);
                _context.SaveChanges();
                Notify("Category Created");
                return RedirectToAction("Category");

            }
            return View(model);
        }

        public IActionResult UpdateCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ServiceCategory serviceCategory = _context.ServiceCategories.FirstOrDefault(i => i.Id == id);
            if (serviceCategory == null)
            {
                return NotFound();
            }
            return View(serviceCategory);
        }


        [HttpPost]
        public IActionResult UpdateCategory(ServiceCategory model)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(model).State = EntityState.Modified;

                _context.SaveChanges();
                Notify("Category Updated");

                return RedirectToAction("Category");

            }
            return View(model);
        }

        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceCategory serviceCategory = _context.ServiceCategories.FirstOrDefault(i => i.Id == id);
            if (serviceCategory == null)
            {
                return NotFound();
            }


            _context.ServiceCategories.Remove(serviceCategory);
            _context.SaveChanges();
            Notify("Category Deleted");
            return RedirectToAction("Category");
        }



        //Benefits
        public IActionResult Benefit()
        {
            List<Benefit> benefits = _context.Benefits.ToList();
            return View(benefits);
        }

        public IActionResult CreateBenefit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBenefit(Benefit model)
        {
            if (ModelState.IsValid)
            {

                _context.Benefits.Add(model);
                _context.SaveChanges();
                Notify("Benefits Created");
                return RedirectToAction("Benefit");

            }
            return View(model);
        }

        public IActionResult UpdateBenefit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Benefit benefit = _context.Benefits.FirstOrDefault(i => i.Id == id);
            if (benefit == null)
            {
                return NotFound();
            }
            return View(benefit);
        }


        [HttpPost]
        public IActionResult UpdateBenefit(Benefit model)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(model).State = EntityState.Modified;

                _context.SaveChanges();
                Notify("Benefits Updated");

                return RedirectToAction("Benefit");

            }
            return View(model);
        }

        public IActionResult DeleteBenefit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Benefit benefit = _context.Benefits.FirstOrDefault(i => i.Id == id);
            if (benefit == null)
            {
                return NotFound();
            }


            _context.Benefits.Remove(benefit);
            _context.SaveChanges();
            Notify("Benefits Deleted");
            return RedirectToAction("Benefit");
        }



        //ServiceOffered
        public IActionResult ServiceOffered()
        {
            List<ServiceOffered> model = _context.ServiceOffereds.ToList();
            return View(model);
        }

        public IActionResult CreateServiceOffered()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateServiceOffered(ServiceOffered model)
        {
            if (ModelState.IsValid)
            {

                _context.ServiceOffereds.Add(model);
                _context.SaveChanges();
                Notify("ServiceOffered Created");
                return RedirectToAction("ServiceOffered");

            }
            return View(model);
        }

        public IActionResult UpdateServiceOffered(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ServiceOffered model = _context.ServiceOffereds.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult UpdateServiceOffered(ServiceOffered model)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(model).State = EntityState.Modified;

                _context.SaveChanges();
                Notify("ServiceOffered Updated");

                return RedirectToAction("ServiceOffered");

            }
            return View(model);
        }

        public IActionResult DeleteServiceOffered(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceOffered model = _context.ServiceOffereds.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }


            _context.ServiceOffereds.Remove(model);
            _context.SaveChanges();
            Notify("ServiceOffered Deleted");
            return RedirectToAction("ServiceOffered");
        }



        //IndustriesServed
        public IActionResult IndustriesServed()
        {
            List<IndustriesServed> model = _context.IndustriesServeds.ToList();
            return View(model);
        }

        public IActionResult CreateIndustriesServed()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateIndustriesServed(IndustriesServed model)
        {
            if (ModelState.IsValid)
            {

                _context.IndustriesServeds.Add(model);
                _context.SaveChanges();
                Notify("Industries Served Created");
                return RedirectToAction("IndustriesServed");

            }
            return View(model);
        }

        public IActionResult UpdateIndustriesServed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IndustriesServed model = _context.IndustriesServeds.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult UpdateIndustriesServed(IndustriesServed model)
        {
            if (ModelState.IsValid)
            {

                _context.Entry(model).State = EntityState.Modified;

                _context.SaveChanges();
                Notify("Industries Served Updated");

                return RedirectToAction("IndustriesServed");

            }
            return View(model);
        }

        public IActionResult DeleteIndustriesServed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IndustriesServed model = _context.IndustriesServeds.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }


            _context.IndustriesServeds.Remove(model);
            _context.SaveChanges();
            Notify("Industries Serveds Deleted");
            return RedirectToAction("IndustriesServed");
        }



        //Transporterium Service
        public IActionResult Transporterium()
        {
            List<TransporteriumService> model = _context.TransporteriumServices.ToList();
            return View(model);
        }

        public IActionResult CreateTransporterium()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTransporterium(TransporteriumService model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg+xml")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Services", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.TransporteriumServices.Add(model);
                            _context.SaveChanges();
                            Notify("Transporterium Service Created");

                            return RedirectToAction("Transporterium");
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


        public IActionResult UpdateTransporterium(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TransporteriumService model = _context.TransporteriumServices.Find(id);
            if (model == null)
            {
                return NotFound();
            }


            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateTransporterium(TransporteriumService model)
        {

            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {

                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg+xml")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Services", model.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Services", fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Entry(model).State = EntityState.Modified;
                            Notify("Transporterium Service Updated");
                            _context.SaveChanges();


                            return RedirectToAction("Transporterium");
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
                    Notify("Transporterium Service Updated");

                    return RedirectToAction("Transporterium");
                }

            }
            return View(model);
        }

        public IActionResult DeleteTransporterium(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TransporteriumService transporterium = _context.TransporteriumServices.FirstOrDefault(i => i.Id == id);
            if (transporterium == null)
            {
                return NotFound();
            }

            //Delete image
            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Services", transporterium.Image);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }


            _context.TransporteriumServices.Remove(transporterium);
            _context.SaveChanges();
            Notify("Transporterium Service Deleted");
            return RedirectToAction("Transporterium");
        }



        //Service Steps for Work
        public IActionResult StepsforWork()
        {
            List<ServiceStepsforWork> model = _context.ServiceStepsforWorks.ToList();
            return View(model);
        }
        
        public IActionResult CreateStepsforWork()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateStepsforWork(ServiceStepsforWork model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title != null)
                {
                    if (model.SubTitle != null)
                    {
                        _context.ServiceStepsforWorks.Add(model);
                        _context.SaveChanges();
                        Notify("Service Steps for Work Created");
                        return RedirectToAction("StepsforWork");
                    }
                    else
                    {
                        Notify("About Services Not Created", notificationType: NotificationType.error);
                        ModelState.AddModelError("SubTitle", "Subtitle must be empty");
                    }

                }
                else
                {
                    Notify("Service Steps for Work Not Created", notificationType: NotificationType.error);
                    ModelState.AddModelError("Title", "Title must be empty");
                }


            }
            return View(model);
        }

        public IActionResult UpdateStepsforWork(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ServiceStepsforWork model = _context.ServiceStepsforWorks.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }


        [HttpPost]
        public IActionResult UpdateStepsforWork(ServiceStepsforWork model)
        {
            if (ModelState.IsValid)
            {
                if (model.Title != null)
                {
                    if (model.SubTitle != null)
                    {
                        _context.Entry(model).State = EntityState.Modified;
                        _context.SaveChanges();
                        Notify("Service Steps for Work Update");

                        return RedirectToAction("StepsforWork");
                    }
                    else
                    {
                        Notify("About Services Not Created", notificationType: NotificationType.error);
                        ModelState.AddModelError("SubTitle", "Subtirle must be empty");
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

        public IActionResult DeleteStepsforWork(int? id)
        {
            if (id == null)
            {
                Notify("Id must be empty", notificationType: NotificationType.error);

                return RedirectToAction("StepsforWork");
            }

            ServiceStepsforWork model = _context.ServiceStepsforWorks.FirstOrDefault(i => i.Id == id);
            if (model == null)
            {
                return NotFound();
            }


            _context.ServiceStepsforWorks.Remove(model);
            _context.SaveChanges();
            Notify("Service Steps for Work Deleted");

            return RedirectToAction("StepsforWork");
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


                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("ImageFile", "Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!");
                    }
                }
                else
                {
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


            PageHeader pageHeader = _context.PageHeaders.Where(p => p.Page == "service").FirstOrDefault();

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

                            _context.SaveChanges();


                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("ImageFile", "Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!");
                    }
                }
                else
                {
                    _context.Entry(model).State = EntityState.Modified;

                    _context.SaveChanges();


                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }

        public IActionResult CreateHeaderDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateHeaderDetails(PageHeader model)
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


                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("ImageFile", "Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!");
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "No Image Found!");
                }

            }
            return View(model);
        }


        public IActionResult UpdateHeaderDetails(int? id)
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


            PageHeader pageHeader = _context.PageHeaders.Where(p => p.Page == "service").FirstOrDefault();

            return View(Header);
        }

        [HttpPost]
        public IActionResult UpdateHeaderDetails(PageHeader model)
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

                            _context.SaveChanges();


                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("ImageFile", "Siz maksimum 2 Mb hecmde fayllari upload ede bilersiniz!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageFile", "Siz yalniz .jpeg, .png, .gif tipli fayllari upload ede bilersiniz!");
                    }
                }
                else
                {
                    _context.Entry(model).State = EntityState.Modified;

                    _context.SaveChanges();


                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }

    }
}
