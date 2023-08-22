using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectShopCMS.DAL.ViewModel
{
    public class ShopCardItemViewModel
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
    }
}
