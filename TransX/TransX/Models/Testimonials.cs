using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Testimonials
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime AddedDate { get; set; }

        public string UserId { get; set; }

        public CustomUser User { get; set; }
    }
}
