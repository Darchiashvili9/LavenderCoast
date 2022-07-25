using System.ComponentModel.DataAnnotations;

namespace LearnMathRu_0._1.Models
{
    public class Customer
    {
        [Key]
        public int ?CustomerId { get; set; }
        [Required]
        public string ?CustomerName { get; set; }
     //   [Required]
    //    public string ?CustomerLName { get; set; }
        [Required]
        public string ?CustomerPhoneNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
