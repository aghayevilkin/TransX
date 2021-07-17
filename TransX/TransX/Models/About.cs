using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }

        [Column(TypeName = "ntext"),Required(ErrorMessage = "Content boş olmamalıdır!")]
        public string Content { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [Required(ErrorMessage = "Image Title boş olmamalıdır!")]
        public string ImageTitle { get; set; }
    }
}
