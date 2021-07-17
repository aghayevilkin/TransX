using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class AboutVideo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Video Link boş olmamalıdır!")]
        public string VideoLink { get; set; }

        [Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }


        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
