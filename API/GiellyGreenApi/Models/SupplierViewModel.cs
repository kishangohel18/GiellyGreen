using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GiellyGreenApi.Models
{
    public class SupplierViewModel
    {
        
        public int SupplierId { get; set; }

        //private string _SupplierName { get; set; }
        //[Required]
        //[RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Suppliername must be alphabet")]
        //[MaxLength(50, ErrorMessage = "Suppliername max length upto 50")]
        //public string SupplierName
        //{
        //    get { return _SupplierName ; }
        //    set
        //    {
        //        _SupplierName = value.Trim();
        //        _SupplierName = Regex.Replace(SupplierName, @"\s+", " ");
        //    }
        //}
        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Suppliername must be alphabet")]
        [MaxLength(50, ErrorMessage = "Suppliername max length upto 50")]
        public string SupplierName { get; set; }

        //public string ReferenceNumber_ { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "ReferenceNumber must be digits")]
        [MaxLength(20, ErrorMessage = "ReferenceNumber max length upto 20")]

        public string ReferenceNumber { get; set; }

        //public string BusinessAddress_ { get; set; }
        [MaxLength(150, ErrorMessage = "BusinessAddress max length upto 150")]
        public string BusinessAddress { get; set; }

        //public string Email_ { get; set; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        //public string Phone_ { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Phone Number must be digits")]
        [MaxLength(15, ErrorMessage = "Phone Number max length upto 15")]
        public string Phone { get; set; }

        //public string TaxReference_ { get; set; }

        [RegularExpression(@"[0-9]{10}", ErrorMessage = "TaxReference must be 10 digit")]
        [MaxLength(10, ErrorMessage = "TaxReference max length upto 10")]
        public string TaxReference { get; set; }

        //public string CompanyRegNumber_ { get; set; }

        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Company Registered Number should be digits")]
        [MaxLength(8, ErrorMessage = "CompanyRegNumber max length upto 8")]
        public string CompanyRegNumber { get; set; }

        //public string CompanyRegAddress_ { get; set; }

        [MaxLength(150, ErrorMessage = "CompanyRegAddress max length upto 150")]
        public string CompanyRegAddress { get; set; }

        //public string VatNumber_ { get; set; }

        //[RegularExpression(@"[GB]([0-9]{9})", ErrorMessage = "VAT number consists of the letters 'GB' followed by nine numbers")]
        [RegularExpression(@"^(GB)(?:[0-9]{12}|[0-9]{9})$", ErrorMessage = "VAT number consists of the letters 'GB' followed by nine numbers")]
        public string VatNumber { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string LogoUrl { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}