using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.ViewModels
{
    public class VmBlog:VmBase
    {
        public Blog Blog { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Blog> RecentPost { get; set; }
        public List<BlogCategory> Categories { get; set; }
        public List<BlogComment> Comments { get; set; }
        public BlogComment Comment { get; set; }
        public List<BlogTag> Tags { get; set; }
        public VmBlog Filter { get; set; }
        public int? catId { get; set; }
    }
}
