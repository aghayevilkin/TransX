using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.ViewModels
{
    public class VmTeam:VmBase
    {
        public List<CustomUser> CustomUser { get; set; }
        public List<AboutServices> AboutServices { get; set; }
        public TeamImage TeamImage { get; set; }
    }
}
