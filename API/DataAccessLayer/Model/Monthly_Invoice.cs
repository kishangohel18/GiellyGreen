//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Monthly_Invoice
    {
        public int MonthlyInvoiceId { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string SuplierName { get; set; }
        public Nullable<decimal> HairService { get; set; }
        public Nullable<decimal> BeautyService { get; set; }
        public Nullable<decimal> Custom1 { get; set; }
        public Nullable<decimal> Custom2 { get; set; }
        public Nullable<decimal> Custom3 { get; set; }
        public Nullable<decimal> Custom4 { get; set; }
        public Nullable<decimal> Custom5 { get; set; }
        public Nullable<decimal> Net { get; set; }
        public Nullable<decimal> Vat { get; set; }
        public Nullable<decimal> Gross { get; set; }
        public Nullable<decimal> AdvancePaid { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<int> InvoiceReference { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<System.DateTime> CurrentMonth { get; set; }
    }
}
