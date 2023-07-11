using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.ViewModels.ProductDiscount
{
    public class UpdateProductDiscountViewModel
    {
        [Display(Name = "کد تخفیف")]
        public string DiscountCode { get; set; } = string.Empty;

        [Display(Name = "درصد تخفیف")]
        [Range(0, 100)]
        public int Percentage { get; set; }

        [Display(Name = "از تاریخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime StartTime { get; set; }

        [Display(Name = "تا تاریخ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime EndTime { get; set; }
    }
}
