using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShopCMS.DAL
{
    public  class ProductCommentMetaData
    {
        public int CommentId { get; set; }
        public Nullable<int> ParentId { get; set; }
        [Display(Name = "محصول")]
        public int ProductId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = " لطفا {0} را وارد نمایید")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = " لطفا {0} را وارد نمایید")]
        [EmailAddress(ErrorMessage = "لطفا ایمیل معتبر وارد نمایید")]
        public string Email { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = " لطفا {0} را وارد نمایید")]
        [DisplayFormat(DataFormatString = "{0:yyyy/mm/dd}", ApplyFormatInEditMode = true)]

        public System.DateTime CreateDate { get; set; }
        [Display(Name = "نظر کاربر")]
        [Required(ErrorMessage = " لطفا {0} را وارد نمایید")]
        public string Comment { get; set; }
    }
    [MetadataType(typeof(ProductCommentMetaData))]
    public partial class ProductComment
    {

    }
}
