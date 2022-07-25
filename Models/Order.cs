using System.ComponentModel.DataAnnotations;

namespace LearnMathRu_0._1.Models
{
    public class Order
    {
        public int? Id { get; set; }
        public int ?CustomerId { get; set; }
        public int ?ProductId { get; set; }
        [Required]
        public bool hasAgreedTurms { get; set; }
        public bool? CustPayment { get; set; }
        public DateTime? orderDate { get; set; }=DateTime.Now;
        public Product ?Product { get; set; }
        public Customer ?Customer { get; set; }
    }
}

//უნდა შეიცვალოს მოდელები, გაკეთდეს ორი პროდუქტი - პროდუქტი და სეირნობა
//ორდერებიდან თარიღები უნდა გადავიდეს სეირნობის პროდუქტში
//ინდექსში უნდა გავიდეს ორი რენდერპარშალი - @Html.Action("პროდუქტპარშალი", new { id = Model }) და @Html.Action("სეირნობაპარშალი", new { id = Model })
//ამ ინდექსიდან უნდა გადანაწილდეს გამოძახების მიხედვით შესაბამისი მოდელის პარშალში

// ორდერის კონტროლერმა უნდა გაანაწილოს return PartialView("პროდუქტპარშალი", personProfile); ან return PartialView("სეირნობაპარშალი",JuridicalPersonProfile);

//აიაქსით თუ გადასცემ ორდერს კონტროლერს რაიმე პარამეტრს პროდუქტმოდელისათვის და სეირნობამოდელისათვის მაშინ პარამეტრის მიხედვით შეძლებ კონტროლერიდან შესაბამისი პარშალის დაბრუნებას
//ზედა კომენტარის მიხედვით დაბრუნდება მაშინ;