using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.ViewModels
{
    public class VmRegister:VmBase
    {
        [Required(ErrorMessage = "Name is required"), MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required"), MaxLength(30)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email is required"), MaxLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required"), MaxLength(30)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required"), MaxLength(30)]
        public string ConfirmPassword { get; set; }
    }
}
