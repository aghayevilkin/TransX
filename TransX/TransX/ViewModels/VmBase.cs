using Microsoft.AspNetCore.Authentication;
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
        public Setting Setting { get; set; }
        public Message Message { get; set; }
        public List<Message> Messages { get; set; }
        public List<Social> Socials { get; set; }
        public List<Blog> Blogs { get; set; }
        public Service Service { get; set; }
        public List<Service> Services { get; set; }
        public List<Request> Requests { get; set; }
        public List<RequestQuote> RequestQuotes { get; set; }
        public List<Subscribe> Subscribes { get; set; }
        public List<Partner> Partners { get; set; }
        public List<CustomUser> CustomUsers { get; set; }
        public CustomUser CustomUser { get; set; }
        public HomeAbout HomeAbout { get; set; }
        public HomeImage HomeImage { get; set; }
        public List<CaseStudies> CaseStudies { get; set; }


        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

    }
}
