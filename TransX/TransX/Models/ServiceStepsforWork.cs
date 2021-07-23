﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class ServiceStepsforWork
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "SubTitle boş olmamalıdır!")]
        public string SubTitle { get; set; }
    }
}
