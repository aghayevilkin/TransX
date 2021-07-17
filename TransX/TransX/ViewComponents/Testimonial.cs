using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;
using TransX.Models;

namespace TransX.ViewComponents
{
    public class Testimonial : ViewComponent
    {
        private readonly AppDbContext _context;
        public Testimonial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            Testimonials test = _context.Testimonials.Include(u=>u.User).FirstOrDefault();
            ViewBag.Testimonial = test.Image;
            ViewBag.TestimonialId = test.Id;

            List<Testimonials> testimonials = _context.Testimonials.Include(u=>u.User).ThenInclude(s=>s.SocialToUsers).Where(c=>c.Content!=null).ToList();

            return View(testimonials);
        }
    }
}
