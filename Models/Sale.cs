//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sale
    {
        public System.Guid Id { get; set; }
        public System.Guid ClientId { get; set; }
        public System.Guid ProductId { get; set; }
        public decimal Sum { get; set; }
        public System.DateTime Date { get; set; }
        public System.Guid CreatedByUserId { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public Nullable<System.Guid> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDateTime { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Manager Manager1 { get; set; }
        public virtual Product Product { get; set; }
    }
}
