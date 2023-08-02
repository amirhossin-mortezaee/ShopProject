using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShopCMS.DAL.ViewModel
{
    public class ForgetPasswordViewModel
    {
        [Display(Name = "ایمیل ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل معتبر وارد نمایید")] 
        public string Email { get; set; }
    }
}
