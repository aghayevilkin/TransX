using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        [Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "SubTitle boş olmamalıdır!")]
        public string SubTitle { get; set; }
    }
}
