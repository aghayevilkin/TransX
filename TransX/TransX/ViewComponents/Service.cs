﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Data;

namespace TransX.ViewComponents
{
    public class Service : ViewComponent
    {
        private readonly AppDbContext _context;
        public Service(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            List<TransX.Models.Service> services = _context.Services.ToList();

            return View(services);
        }
    }
}
