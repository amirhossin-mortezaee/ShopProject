using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShopCMS.DAL
{
    class ProductGropsMetaData
    {
        public int GroupId { get; set; }
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }
        [Display(Name = "عنوان زیر گروه ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Nullable<int> ParentId { get; set; }
        [MetadataType(typeof(ProductGropsMetaData))]
        public partial class ProductGrops
        {

        }
    }
}
