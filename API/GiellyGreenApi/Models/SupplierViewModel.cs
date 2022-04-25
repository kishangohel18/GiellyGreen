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

        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Suppliername must be alphabet")]
        [MaxLength(50, ErrorMessage = "Suppliername max length upto 50")]
        public string SupplierName { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Supplier Reference must be alphanumeric and space not allowed")]
        [MaxLength(15, ErrorMessage = "Supplier Reference max length upto 15")]
        public string SupplierReference { get; set; }

        [MaxLength(150, ErrorMessage = "Business address max length upto 150")]
        public string BusinessAddress { get; set; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        [RegularExpression(@"[ 0-9 ]{0,15}", ErrorMessage = "Phone Number must be digits and upto 15")]       
        public string Phone { get; set; }

        [RegularExpression(@"^[ a-zA-Z0-9 ]{0,15}$", ErrorMessage = "Tax reference must be alphanumeric and upto 15")]
        public string TaxReference { get; set; }

        [RegularExpression(@"^[ 0-9 ]{0,15}$", ErrorMessage = "Company registered number must be digits and upto 15")]
        public string CompanyRegNumber { get; set; }

        [MaxLength(150, ErrorMessage = "Company registered address max length upto 150")]
        public string CompanyRegAddress { get; set; }

        [RegularExpression(@"^[ a-zA-Z0-9 ]{0,15}$", ErrorMessage = "VAT number must be alphanumeric and max length upto 15")]
        public string VatNumber { get; set; }

        public string LogoUrl { get; set; }
        public Nullable<bool> IsActive { get; set; }


    }
}