using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.ViewModels
{
    public class VmAbout:VmBase
    {
        public About About { get; set; }
        public AboutMission AboutMission { get; set; }
        public AboutVideo AboutVideo { get; set; }
        public List<AboutServices> AboutServices { get; set; }
        public List<Achievement> Achievements { get; set; }
        public List<History> Histories { get; set; }
        public List<CustomUser> CustomUser { get; set; }
    }
}
