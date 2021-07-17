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
using TransX.Data;
using TransX.Models;
using TransX.ViewModels;

namespace TransX.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Moderator")]
    public class BlogController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BlogController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            List<BlogCategory> categories = _context.BlogCategories.ToList();
            categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
            ViewBag.Categories = categories;
            categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
            ViewBag.Categories = categories;
            List<BlogTag> tags = _context.BlogTags.ToList();
            ViewBag.Tags = tags;
            VmBlog model = new VmBlog()
            {
                Blogs = _context.Blogs.Include(g => g.Category).Include(u => u.User).Include(tb => tb.TagToBlogs).ThenInclude(t => t.Tag).ToList(),
                Blog = new Blog(),
                Categories= ViewBag.Categories,
                Tags= ViewBag.Tags,
                pageHeader = _context.PageHeaders.Where(p => p.Page == "blog").FirstOrDefault(),
                pageHeaderDetails = _context.PageHeaders.Where(p => p.Page == "blogdetails").FirstOrDefault(),
            };
            return View(model);
        }

        public IActionResult ViewAll()
        {
            IList<Blog> blogs = _context.Blogs.Include(g => g.Category).Include(u => u.User).Include(tb => tb.TagToBlogs).ThenInclude(t => t.Tag).ToList();
            return View(blogs);
        }

        //Create
        public IActionResult Create()
        {
            List<BlogCategory> categories = _context.BlogCategories.ToList();
            categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
            ViewBag.Categories = categories;

            List<BlogTag> tags = _context.BlogTags.ToList();
            ViewBag.Tags = tags;

            VmBlog model = new VmBlog()
            {
                Blogs = _context.Blogs.Include(g => g.Category).Include(u => u.User).Include(tb => tb.TagToBlogs).ThenInclude(t => t.Tag).ToList(),
                Blog = new Blog(),
                Categories = ViewBag.Categories,
                Tags = ViewBag.Tags,
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult Create(VmBlog model)
        {
            if (ModelState.IsValid)
            {
                if (model.Blog.CategoryId == 0)
                {
                    ModelState.AddModelError("CategoryId", "Categoriya secmelisiniz!");
                    List<BlogCategory> categories = _context.BlogCategories.ToList();
                    categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
                    ViewBag.Categories = categories;
                    List<BlogTag> tags = _context.BlogTags.ToList();
                    ViewBag.Tags = tags;
                    return View(model);
                }
                if (model.Blog.ImageFile != null)
                {
                    if (model.Blog.ImageFile.ContentType == "image/png" || model.Blog.ImageFile.ContentType == "image/jpeg" || model.Blog.ImageFile.ContentType == "image/gif" || model.Blog.ImageFile.ContentType == "image/svg")
                    {
                        if (model.Blog.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.Blog.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Blogs", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.Blog.ImageFile.CopyTo(stream);
                            }

                            model.Blog.Image = fileName;
                            model.Blog.UserId = _userManager.GetUserId(User);
                            model.Blog.AddedDate = DateTime.Now;

                            _context.Blogs.Add(model.Blog);
                            _context.SaveChanges();

                            if (model.Blog.TagIds == null)
                            {
                                ModelState.AddModelError("TagIds", "Tag secmelisiniz!");
                                return View(model);
                            }
                            else
                            {
                                foreach (var item in model.Blog.TagIds)
                                {
                                    TagToBlog tagToBlog = new TagToBlog()
                                    {
                                        BlogId = model.Blog.Id,
                                        TagId = item
                                    };

                                    _context.TagToBlogs.Add(tagToBlog);
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
                    ModelState.AddModelError("ImageFile", "No Image Found!");
                }
                
            }

            return View(model);
        }



        //Update
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Blog blog = _context.Blogs.Include(t => t.TagToBlogs).FirstOrDefault(i => i.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            List<BlogCategory> categories = _context.BlogCategories.ToList();
            categories.Insert(0, new BlogCategory() { Id = 0, Name = "Select" });
            ViewBag.Categories = categories;

            List<BlogTag> tags = _context.BlogTags.ToList();
            ViewBag.Tags = tags;

            List<int> tagIds = new List<int>();

            foreach (var item in blog.TagToBlogs)
            {
                tagIds.Add(item.TagId);
            }

            blog.TagIds = tagIds.ToArray();

            return View(blog);
        }



        [HttpPost]
        public IActionResult Update(Blog model)
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
                        List<BlogTag> tags = _context.BlogTags.ToList();
                        ViewBag.Tags = tags;
                        return View(model);
                    }
                    if (model.ImageFile.ContentType == "image/png" || model.ImageFile.ContentType == "image/jpeg" || model.ImageFile.ContentType == "image/gif" || model.ImageFile.ContentType == "image/svg")
                    {
                        if (model.ImageFile.Length <= 2097152)
                        {
                            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Blogs", model.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "-" + model.ImageFile.FileName;
                            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Blogs", fileName);


                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(stream);
                            }

                            model.Image = fileName;

                            _context.Entry(model).State = EntityState.Modified;
                            _context.Entry(model).Property(a => a.UserId).IsModified = false;
                            _context.Entry(model).Property(a => a.AddedDate).IsModified = false;

                            _context.SaveChanges();


                            foreach (var item in _context.Blogs.Include(a => a.TagToBlogs).FirstOrDefault(i => i.Id == model.Id).TagToBlogs)
                            {
                                _context.TagToBlogs.Remove(item);
                            }
                            _context.SaveChanges();


                            foreach (var item in model.TagIds)
                            {
                                TagToBlog tagToBlog = new TagToBlog()
                                {
                                    BlogId = model.Id,
                                    TagId = item
                                };

                                _context.TagToBlogs.Add(tagToBlog);
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
                        List<BlogTag> tags = _context.BlogTags.ToList();
                        ViewBag.Tags = tags;
                        return View(model);
                    }

                    _context.Entry(model).State = EntityState.Modified;
                    _context.Entry(model).Property(a => a.UserId).IsModified = false;
                    _context.Entry(model).Property(a => a.AddedDate).IsModified = false;

                    _context.SaveChanges();


                    foreach (var item in _context.Blogs.Include(a => a.TagToBlogs).FirstOrDefault(i => i.Id == model.Id).TagToBlogs)
                    {
                        _context.TagToBlogs.Remove(item);
                    }
                    _context.SaveChanges();


                    foreach (var item in model.TagIds)
                    {
                        TagToBlog tagToBlog = new TagToBlog()
                        {
                            BlogId = model.Id,
                            TagId = item
                        };

                        _context.TagToBlogs.Add(tagToBlog);
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

            Blog blog = _context.Blogs.Include(a => a.TagToBlogs).FirstOrDefault(i => i.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            //Delete image
            string oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/Images/Blogs", blog.Image);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }


            //Delete tags
            foreach (var item in blog.TagToBlogs)
            {
                _context.TagToBlogs.Remove(item);
            }

            _context.Blogs.Remove(blog);
            _context.SaveChanges();

            return RedirectToAction("Index");
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
                if (model.ImageFile!=null)
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


            PageHeader pageHeader = _context.PageHeaders.Where(p => p.Page == "about").FirstOrDefault();

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
