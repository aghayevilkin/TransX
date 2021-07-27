using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.ViewModels
{
    public class VmRequest:VmBase
    {
        public Request Request { get; set; }
        public List<Request> Requests { get; set; }
        public List<City> Cities { get; set; }
    }
}
