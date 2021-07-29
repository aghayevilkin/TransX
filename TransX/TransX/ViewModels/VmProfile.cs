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
        public IList<SavedBlogs> SavedBlogs { get; set; }
        public IList<RequestQuote> RequestQuotes { get; set; }
        public IList<Request> Requests { get; set; }
        public List<BlogTag> Tags { get; set; }
        public VmChangePassword VmChangePassword { get; set; }
        public VmAddPassword VmAddPassword { get; set; }
        public SocialToUser SocialToUser { get; set; }
        public List<BlogCategory> Categories { get; set; }
    }
}
