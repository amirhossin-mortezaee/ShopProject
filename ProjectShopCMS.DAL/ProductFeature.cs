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
    
    public partial class ProductFeature
    {
        public int ProductFeatureId { get; set; }
        public int ProductId { get; set; }
        public int FeatureId { get; set; }
        public string Value { get; set; }
    
        public virtual Feature Feature { get; set; }
        public virtual Products Products { get; set; }
    }
}
