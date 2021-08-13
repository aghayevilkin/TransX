using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;
using TransX.Models;
using TransX.ViewModels;

namespace TransX.Controllers
{
    public class BlogController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public BlogController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index(int? id, int? tagId, int? year, int? month, VmBlog VmFilter, string searchData, int page = 1)
        {
            var userId = _userManager.GetUserId(User);
            TempData["Controller"] = "Blog";

            decimal pageItemCount = 6;
            ViewBag.Page = "blog";
            ViewBag.UserId = userId;


            ViewBag.categoryId = id;
            IList<Blog> blogs = _context.Blogs.Include(saved=>saved.SavedBlogs).Include(u => u.User).ThenInclude(us=>us.SocialToUsers).ThenInclude(soc=>soc.Social).Where(b => (id != null ? b.CategoryId == id : true) &&
                                                          (tagId != null ? b.TagToBlogs.Any(t => t.TagId == tagId) : true) &&
                                                          (year != null ? b.AddedDate.Year == year : true) &&
                                                          (month != null ? b.AddedDate.Month == month : true) &&
                                                          (b.BlogStatus==BlogStatus.Active)
                                                          ).Where(sr =>
                                                                  ((searchData != null ? sr.Title.Contains(searchData) : true) || (searchData != null ? sr.Category.Name.Contains(searchData) : true)) &&
                                                                  (VmFilter.catId != null ? sr.CategoryId == VmFilter.catId : true))
                                                     .ToList();


            decimal b = Math.Ceiling(blogs.Count / pageItemCount);
            ViewBag.PageCount = Convert.ToInt32(b);
            ViewBag.ActivePage = page;
            ViewBag.prewPage = page-1;
            ViewBag.nextPage = page+1;

            List<Blog> products = blogs.OrderBy(p => p.Id)
                                                     .OrderByDescending(added=>added.AddedDate).Skip((page - 1) * (int)pageItemCount).Take((int)pageItemCount)
                                                     .ToList();
            VmBlog model = new VmBlog()
            {
                Blogs = products,
                RecentPost = _context.Blogs.Include(c => c.User).Where(s => s.BlogStatus == BlogStatus.Active).OrderByDescending(o => o.AddedDate).Take(4).ToList(),
                Categories = _context.BlogCategories.Include(b => b.Blogs).Where(bb => bb.Blogs.Any(s => s.BlogStatus == BlogStatus.Active)).ToList(),
                Comments = _context.BlogComments.ToList(),
                Tags = _context.BlogTags.Include(b => b.TagToBlogs).ThenInclude(bl => bl.Blog).ToList(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "blog").FirstOrDefault(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
            };

            return View(model);
        }



        public IActionResult Details(int id)
        {
            TempData["Controller"] = "Blog";
            ViewBag.Page = "blog";
            int catId = _context.Blogs.Find(id).CategoryId;
            var userIdd= _userManager.GetUserId(User);
            var blogUser = _context.Blogs.Find(id).UserId;
            ViewBag.categoryId = catId;
            VmBlog model = new VmBlog()
            {
                Blog = _context.Blogs.Include(t => t.TagToBlogs).ThenInclude(t => t.Tag).Include(u => u.User).ThenInclude(us => us.SocialToUsers).ThenInclude(soc=>soc.Social).FirstOrDefault(b => b.Id == id),
                Blogs = _context.Blogs.Include(u => u.User).ThenInclude(us => us.SocialToUsers).ThenInclude(s => s.Social).Where(aa=>aa.User.SocialToUsers.Any(bb=>bb.User.Id==blogUser)).ToList(),
                Categories = _context.BlogCategories.Include(b => b.Blogs).ToList(),
                Comments = _context.BlogComments.Include(u => u.User).Where(c => c.BlogId == id).ToList(),
                RecentPost = _context.Blogs.Where(s => s.BlogStatus == BlogStatus.Active).OrderByDescending(o => o.AddedDate).Take(3).ToList(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "blogdetails").FirstOrDefault(),
                pageHeaderDetails = _context.PageHeaders.Where(p => p.Page == "blogdetails").FirstOrDefault(),
                Tags = _context.BlogTags.Include(tb=>tb.TagToBlogs).ThenInclude(b=>b.Blog).Where(t=>t.TagToBlogs.Any(tbb=>tbb.BlogId==id)).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
            };


            return View(model);
        }



        public IActionResult CommentCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CommentCreate(BlogComment model)
        {
            int bId = 0;
            if (ModelState.IsValid)
            {
                bId = model.BlogId;
                model.UserId = _userManager.GetUserId(User);
                model.AddedDate = DateTime.Now;
                _context.BlogComments.Add(model);
                _context.SaveChanges();
                Notify("Add Comments successfully");
            }
            return RedirectToAction("Details", new { id = bId });

        }


        public JsonResult addToBookmarked(int? blogId)
        {
            var userId = _userManager.GetUserId(User);
            if (blogId==null)
            {
                return Json(404);
            }

            bool isExist = _context.SavedBlogs.Any(aa => aa.BlogId == blogId && aa.UserId == userId);

            if (!isExist)
            {
                SavedBlogs model = new SavedBlogs()
                {
                    BlogId = (int)blogId,
                    UserId = userId,
                    AddedDate = DateTime.Now,
                };

                _context.SavedBlogs.Add(model);
                _context.SaveChanges();
            }
            else
            {
                return Json(404);
            }

            

            return Json(200);
        }

        public JsonResult removeFromBookmarked(int? blogId) 
        {
            var userId = _userManager.GetUserId(User);
            if (blogId == null)
            {
                return Json(404);
            }

            bool isExist = _context.SavedBlogs.Any(aa => aa.BlogId == blogId && aa.UserId == userId);

            if (isExist)
            {
                SavedBlogs model = _context.SavedBlogs.FirstOrDefault(aa => aa.BlogId == blogId && aa.UserId == userId);

                _context.SavedBlogs.Remove(model);
                _context.SaveChanges();
            }
            else
            {
                return Json(404);
            }
            return Json(200);
        }

    }
}
