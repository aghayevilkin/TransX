using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }

        public int CountNum { get; set; }

        [Required(ErrorMessage = "CountSub boş olmamalıdır!")]
        public string CountSub { get; set; }

        [Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }
    }
}
