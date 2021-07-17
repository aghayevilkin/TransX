using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class AboutServices
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500), Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Icon boş olmamalıdır!")]
        public string Icon { get; set; }

    }
}
