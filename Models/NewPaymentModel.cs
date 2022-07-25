using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LearnMathRu_0._1.Models
{
    public class NewPaymentModel:PageModel
    {

        //tmp cc:1111111111111026

        [BindProperty, Required]
        public string ShopId { get; set; } = "915224";

        [BindProperty, Required]
        public string SecretKey { get; set; } = "test_ncQjpyhbpbIHG67Btbk5h4Sf3td9Lyd7tG0pMrvFZJI";

        [BindProperty, Range(1, 2000), Required]
        public decimal Amount { get; set; } = 999;

        [BindProperty] public string Payment { get; set; }
    }
}
