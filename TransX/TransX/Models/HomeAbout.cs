using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class HomeAbout
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }

        [Column(TypeName = "ntext"), Required(ErrorMessage = "Content boş olmamalıdır!")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Experiance Transportation boş olmamalıdır!")]
        public int ExperianceTransportation { get; set; }

        [Required(ErrorMessage = "Skilled Drivers boş olmamalıdır!")]
        public int SkilledDrivers { get; set; }

        [Required(ErrorMessage = "Milles Driven Per Year boş olmamalıdır!")]
        public int MillesDrivenPerYear { get; set; }
    }
}
