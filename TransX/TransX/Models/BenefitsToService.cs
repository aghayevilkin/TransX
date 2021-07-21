using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class BenefitsToService
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }


        [ForeignKey("Benefit")]
        public int BenefitId { get; set; }
        public Benefit Benefit { get; set; }
    }
}
