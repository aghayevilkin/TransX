using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.ViewModels
{
    public class VmService : VmBase
    {
        public Service Service { get; set; }
        public List<Service> Services { get; set; }
        public List<ServiceCategory> Categories { get; set; }
        public List<Benefit> Benefits { get; set; }
        public List<ServiceOffered> ServiceOffereds { get; set; }
        public List<IndustriesServed> IndustriesServeds { get; set; }
        public VmService Filter { get; set; }
        public int? catId { get; set; }
    }
}
