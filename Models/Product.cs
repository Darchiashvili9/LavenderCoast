using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMathRu_0._1.Models
{
    public class Product
    {
      //  [NotMapped]
        public int? ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        public int? ProductQuantity { get; set; }


        //ეს ველია გასამიჯნი ვიზიტის და მასტერკლასის ორი ველი უნდა იყოს აქ
        //მიგრაციებია ასაწევი და მაგის მიხედვით უნდა გაკეთდეს ახალი ვერსია
        //ნუგეტებში იქნება ენთითის პაკეტები შესაცვლელი თულსით და სქლსერვერით
        //ახალი ვერსია უნდა აიწიოს და იმაზეა სამუშაო;

        public DateTime? VisitDate { get; set; }
        public DateTime? MasterclassDate { get; set; }


        //     public string? OrderReceiveDate { get; set; }
        public string? OrderReceiveTime { get; set; }


        //   public VisitOnSite? visistProduct { get; set; }
    }
}
