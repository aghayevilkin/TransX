using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class PageHeader
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required"),MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subtitle is required")]
        public string Subtitle { get; set; }

        [MaxLength(250)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [MaxLength(25)]
        public string Page { get; set; }
    }
}
