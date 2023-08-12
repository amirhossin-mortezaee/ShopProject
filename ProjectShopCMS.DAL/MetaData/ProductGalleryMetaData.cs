using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShopCMS.DAL
{
    public class ProductGalleryMetaData
    {
        public int GalleryId { get; set; }
        [Display(Name = "نام محصول")]
        public int ProductId { get; set; }
        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "نام تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GalleryTitle { get; set; }
    }
    [MetadataType(typeof(ProductGalleryMetaData))]
    public partial class ProductGallery
    {

    }
}
