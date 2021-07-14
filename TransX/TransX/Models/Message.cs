using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "Maksimum 20 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Name boş olmamalıdır!")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Maksimum 50 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Email boş olmamalıdır!")]
        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = "Maksimum 50 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Subject boş olmamalıdır!")]
        public string Subject { get; set; }
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
