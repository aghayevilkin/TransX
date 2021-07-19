﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransX.Models;

namespace TransX.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CustomUser> CustomUsers { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<SocialToUser> SocialToUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<TagToBlog> TagToBlogs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<PageHeader> PageHeaders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Testimonials> Testimonials { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<AboutServices> AboutServices { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<AboutMission> AboutMissions { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutVideo> AboutVideos { get; set; }
        public DbSet<TeamImage> TeamImages { get; set; }
    }
}
