using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.ViewModels
{
    public class VmLogin:VmBase
    {
        [Required(ErrorMessage = "Email is required"), MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(30)]
        public string Password { get; set; }
    }
}
