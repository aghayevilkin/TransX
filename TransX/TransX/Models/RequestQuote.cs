using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class RequestQuote
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Where from boş olmamalıdır!")]
        public string From { get; set; }

        [Required(ErrorMessage = "To where boş olmamalıdır!")]
        public string To { get; set; }
        public DateTime When { get; set; }
        public DateTime AddedDate { get; set; }

        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public CustomUser User { get; set; }
    }
}
