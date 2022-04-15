using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GiellyGreenApi.Models
{
    public class SupplierViewModel
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

    }
}