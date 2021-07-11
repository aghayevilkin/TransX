using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class BlogComment
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(2000)]
        public string Message { get; set; }
        public int BlogId { get; set; }

        [ForeignKey("BlogId")]
        public Blog Blog { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public CustomUser User { get; set; }

        public int? CommentId { get; set; }
        [ForeignKey("CommentId")]
        public BlogComment ParentComment { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
