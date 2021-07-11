using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.ViewModels
{
    public class VmBase
    {
        public VmLogin LoginViewModel { get; set; }
        public VmRegister RegisterViewModel { get; set; }
        public PageHeader pageHeader { get; set; }
        public PageHeader pageHeaderDetails { get; set; }

    }
}
