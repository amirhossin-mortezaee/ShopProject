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
    
    public partial class ProductComment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductComment()
        {
            this.ProductComment1 = new HashSet<ProductComment>();
        }
    
        public int CommentId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Comment { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductComment> ProductComment1 { get; set; }
        public virtual ProductComment ProductComment2 { get; set; }
        public virtual Products Products { get; set; }
    }
}
