using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.ViewModels
{
    public class VmFaq : VmBase
    {
        public List<Faq> Faqs { get; set; }
    }
}
