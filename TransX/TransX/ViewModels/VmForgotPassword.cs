﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.ViewModels
{
    public class VmForgotPassword:VmBase
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
