using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "Maksimum 20 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Name boş olmamalıdır!")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Maksimum 50 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Email boş olmamalıdır!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Delivery City boş olmamalıdır!")]
        public string DeliveryCity { get; set; }

        [Required(ErrorMessage = "Departure City boş olmamalıdır!")]
        public string DepartureCity { get; set; }

        [Required(ErrorMessage = "Weight boş olmamalıdır!")]
        public int Weight { get; set; }
        
        [Required(ErrorMessage = "Height boş olmamalıdır!")]
        public int Height { get; set; }
        
        [Required(ErrorMessage = "Widhth boş olmamalıdır!")]
        public int Widhth { get; set; }
        
        [Required(ErrorMessage = "Lenght boş olmamalıdır!")]
        public int Lenght { get; set; }

        public string InsuranceOrPackaging { get; set; }

        public DateTime AddedDate { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public CustomUser User { get; set; }
    }
}
