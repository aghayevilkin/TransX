using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class BlogTag
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "Maksimum 30 charakter daxil edə bilərsiniz"), Required(ErrorMessage = "Ad boş olmamalıdır!")]
        public string Name { get; set; }
        public List<TagToBlog> TagToBlogs { get; set; }
    }
}
