using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string LogoWhite { get; set; }
        public string LogoBlack { get; set; }
        [MaxLength(250, ErrorMessage = "Maksimum 250 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Location boş olmamalıdır!")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Phone boş olmamalıdır!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email boş olmamalıdır!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Openning Hours boş olmamalıdır!")]
        public string OpenningHours { get; set; }
        [Required(ErrorMessage = "Footer boş olmamalıdır!")]
        public string Footer { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public IFormFile ImageFileTwo { get; set; }
    }
}
