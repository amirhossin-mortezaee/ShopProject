using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShopCMS.DAL.ViewModel
{
    public class RecoveryPasswordViewModel
    {
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه های عبور با یکدیگر مطابقت ندارند")]
        public string RePassword { get; set; }
    }
}
