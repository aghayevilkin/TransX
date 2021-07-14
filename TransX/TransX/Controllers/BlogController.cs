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
    public class BlogController : Controller
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
            TempData["Controller"] = "Blog";

            decimal pageItemCount = 4;
            ViewBag.Page = "blog";
            ViewBag.categoryId = id;
            IList<Blog> blogs = _context.Blogs.Include(u => u.User).Where(b => (id != null ? b.CategoryId == id : true) &&
                                                          (tagId != null ? b.TagToBlogs.Any(t => t.TagId == tagId) : true) &&
                                                          (year != null ? b.AddedDate.Year == year : true) &&
                                                          (month != null ? b.AddedDate.Month == month : true)
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
                                                     .Skip((page - 1) * (int)pageItemCount).Take((int)pageItemCount)
                                                     .ToList();
            VmBlog model = new VmBlog()
            {
                Blogs = products,
                RecentPost = _context.Blogs.Include(c => c.User).OrderByDescending(o => o.AddedDate).Take(4).ToList(),
                Categories = _context.BlogCategories.Include(b => b.Blogs).ToList(),
                Comments = _context.BlogComments.ToList(),
                Tags = _context.BlogTags.Include(b => b.TagToBlogs).ThenInclude(bl => bl.Blog).ToList(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "blog").FirstOrDefault(),
                Setting = _context.Settings.FirstOrDefault(),
            };

            return View(model);
        }



        public IActionResult Details(int id)
        {
            TempData["Controller"] = "Blog";
            ViewBag.Page = "blog";
            int catId = _context.Blogs.Find(id).CategoryId;
            var userIdd= _userManager.GetUserId(User);
            VmBlog model = new VmBlog()
            {
                Blog = _context.Blogs.Include(t => t.TagToBlogs).ThenInclude(t => t.Tag).Include(u => u.User).ThenInclude(us => us.SocialToUsers).FirstOrDefault(b => b.Id == id),
                Blogs = _context.Blogs.Include(u => u.User).ThenInclude(us => us.SocialToUsers).ThenInclude(s => s.Social).Where(aa=>aa.User.SocialToUsers.Any(bb=>bb.User.Id==userIdd)).ToList(),
                Categories = _context.BlogCategories.Include(b => b.Blogs).ToList(),
                Comments = _context.BlogComments.Include(u => u.User).Where(c => c.BlogId == id).ToList(),
                RecentPost = _context.Blogs.OrderByDescending(o => o.AddedDate).Take(3).ToList(),
                pageHeader = _context.PageHeaders.Where(p => p.Page == "blogdetails").FirstOrDefault(),
                pageHeaderDetails = _context.PageHeaders.Where(p => p.Page == "blogdetails").FirstOrDefault(),
                Tags = _context.BlogTags.Include(tb=>tb.TagToBlogs).ThenInclude(b=>b.Blog).ToList(),
                Setting = _context.Settings.FirstOrDefault(),
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
                ViewBag.Message = "Test Alert";
            }
            return RedirectToAction("Details", new { id = bId });

        }

    }
}
