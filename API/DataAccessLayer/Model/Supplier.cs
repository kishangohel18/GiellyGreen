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
    
    public partial class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ReferenceNumber { get; set; }
        public string BusinessAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TaxReference { get; set; }
        public string CompanyRegNumber { get; set; }
        public string CompanyRegAddress { get; set; }
        public string VatNumber { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string LogoUrl { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsInvoiced { get; set; }
    }
}
