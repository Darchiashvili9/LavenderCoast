using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMathRu_0._1.Models
{
    [NotMapped]
    public class DatabaseModel
    {
        public List<Order>? ord { get; set; }
        public List<Customer>? cust { get; set; }
        public List<Product>? prod { get; set; }
    }
}
