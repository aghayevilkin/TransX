using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class ServiceOffered
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name boş olmamalıdır!")]
        public string Name { get; set; }
        public List<ServiceOfferedToService> ServiceOfferedToServices { get; set; }
    }
}
