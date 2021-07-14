using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.ViewModels
{
    public class VmProfile:VmBase
    {
        public Blog Post { get; set; }
        public CustomUser User { get; set; }
        public IList<CustomUser> UserS { get; set; }
        public IList<Blog> Posts { get; set; }
        public List<BlogTag> Tags { get; set; }
        public VmChangePassword VmChangePassword { get; set; }
        public VmAddPassword VmAddPassword { get; set; }
    }
}
