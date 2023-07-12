using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShopCMS.DAL
{
    public class SliderMetaData
    {
        [Key]
        public int SliderId { get; set; }
        [Display(Name = "عنوان تخفیف ها")]
        public string DiscountTitle { get; set; }
        [Display(Name = "عنوان اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ شروع اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString = "{0: yyyy/mm/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime StartSliderDate { get; set; }
        [Display(Name = "تاریخ پایان اسلایدر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DisplayFormat(DataFormatString = "{0: yyyy/mm/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime EndSliderDate { get; set; }
        [Display(Name = "فعال / فعال نبودن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsActive { get; set; }
    }
    [MetadataType(typeof(SliderMetaData))]
    public partial class Slider
    {

    }

}
