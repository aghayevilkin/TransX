using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;

namespace TransX.ViewComponents
{
    public class Partners:ViewComponent
    {
        private readonly AppDbContext _context;
        public Partners(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            List<TransX.Models.Partner> partners = _context.Partners.ToList();

            return View(partners);
        }
    }
}
