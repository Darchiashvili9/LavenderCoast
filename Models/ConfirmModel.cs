using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Yandex.Checkout.V3;

namespace LearnMathRu_0._1.Models
{
    public class ConfirmModel : PageModel
    {
        [BindProperty] public int Id { get; set; }
        [BindProperty] public string Action { get; set; }
        public string Payment { get; set; }
        public bool AllowConfirm { get; set; }
        public string Message { get; set; }
    }
}
