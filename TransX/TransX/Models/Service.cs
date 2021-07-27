using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransX.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500), Required(ErrorMessage = "Title boş olmamalıdır!")]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [Column(TypeName = "ntext"), Required(ErrorMessage = "Content boş olmamalıdır!")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Price Per Km Secmelisiniz!")]
        public double PricePerKm { get; set; }

        public DateTime AddedDate { get; set; }

        [Required(ErrorMessage = "Category Secmelisiniz!")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public ServiceCategory Category { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public CustomUser User { get; set; }

        [NotMapped]
        public int[] BenefitIds { get; set; }
        [NotMapped]
        public int[] ServiceOfferedIds { get; set; }
        [NotMapped]
        public int[] IndustriesServedIds { get; set; }
        public List<BenefitsToService> BenefitsToServices { get; set; }
        public List<ServiceOfferedToService> ServiceOfferedToServices { get; set; }
        public List<IndustriesServedToService> IndustriesServedToServices { get; set; }
        public List<RequestQuote> RequestQuotes { get; set; }
        public List<Request> Requests { get; set; }
    }
}
