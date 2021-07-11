using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Subscribe
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Maksimum 50 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Email boş olmamalıdır!")]
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
