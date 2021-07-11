using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class CustomUser : IdentityUser
    {
        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string Surname { get; set; }
        public string Image { get; set; }
        [Required]
        public bool IsVerify { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public string RoleId { get; set; }

        [NotMapped]
        public List<SelectListItem> Roles { get; set; }

        public List<SocialToUser> SocialToUsers { get; set; }
        public List<BlogComment> BlogComments { get; set; }
    }
}
