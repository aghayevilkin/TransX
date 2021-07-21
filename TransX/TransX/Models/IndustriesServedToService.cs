using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class IndustriesServedToService
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }


        [ForeignKey("IndustriesServed")]
        public int IndustriesServedId { get; set; }
        public IndustriesServed IndustriesServed { get; set; }
    }
}
