using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Faq
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required"), MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Subtitle is required"),]
        public string Subtitle { get; set; }

        [Required]
        public bool IsQuestions { get; set; }
    }
}
