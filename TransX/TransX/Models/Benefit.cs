using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Benefit
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "Maksimum 30 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Icon boş olmamalıdır!")]
        public string Icon { get; set; }
        public List<BenefitsToService> BenefitsToServices { get; set; }
    }
}
