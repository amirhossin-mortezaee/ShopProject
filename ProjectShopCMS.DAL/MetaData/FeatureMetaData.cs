using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShopCMS.DAL
{
    public class FeatureMetaData
    {
        [Key]
        public int FeatureId { get; set; }
        [Display(Name = "عنوان ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FetureTitle { get; set; }
    }

    [MetadataType(typeof(FeatureMetaData))]
    public partial class Feature
    {

    }
}
