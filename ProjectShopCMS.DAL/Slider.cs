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
    
    public partial class Slider
    {
        public int SliderId { get; set; }
        public string DiscountTitle { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public System.DateTime StartSliderDate { get; set; }
        public System.DateTime EndSliderDate { get; set; }
        public bool IsActive { get; set; }
    }
}
