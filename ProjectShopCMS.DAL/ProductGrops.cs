//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectShopCMS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductGrops
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductGrops()
        {
            this.ProductGrops1 = new HashSet<ProductGrops>();
            this.SelectedProductCateGory = new HashSet<SelectedProductCateGory>();
        }
    
        public int GroupId { get; set; }
        public string GroupTitle { get; set; }
        public Nullable<int> ParentId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductGrops> ProductGrops1 { get; set; }
        public virtual ProductGrops ProductGrops2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelectedProductCateGory> SelectedProductCateGory { get; set; }
    }
}
