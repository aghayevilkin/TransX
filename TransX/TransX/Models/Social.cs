﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Social
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Icon { get; set; }

        [MaxLength(250)]
        public string Link { get; set; }


        public List<SocialToUser> SocialToUsers { get; set; }
    }
}
