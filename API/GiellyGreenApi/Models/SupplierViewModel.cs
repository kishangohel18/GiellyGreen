﻿using System;
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

        private string _SupplierName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Suppliername must be alphabet")]
        [MaxLength(50, ErrorMessage = "Suppliername max length upto 50")]
        public string SupplierName
        {
            get { return _SupplierName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _SupplierName = value.Trim();
                    _SupplierName = Regex.Replace(SupplierName, @"\s+", " ");
                }
            }
        }


        public string _ReferenceNumber { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Reference number must be alphanumeric")]
        [MaxLength(15, ErrorMessage = "Reference number max length upto 15")]
        public string ReferenceNumber
        {
            get { return _ReferenceNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _ReferenceNumber = value.Trim();
                    _ReferenceNumber = Regex.Replace(ReferenceNumber, @"\s+", " ");
                }
            }
        }

        public string _BusinessAddress { get; set; }
        [MaxLength(150, ErrorMessage = "Business address max length upto 150")]
        public string BusinessAddress
        {
            get { return _BusinessAddress; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _BusinessAddress = value.Trim();
                    _BusinessAddress = Regex.Replace(BusinessAddress, @"\s+", " ");
                }
            }
        }
        //public string _Email { get; set; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Email not valid")]
        public string Email { get; set; }

        public string _Phone { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessage = "Phone Number must be digits")]
        [MinLength(7, ErrorMessage = "Phone Number min length is 7")]
        [MaxLength(15, ErrorMessage = "Phone Number max length is 15")]
        public string Phone
        {
            get { return _Phone; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _Phone = value.Trim();
                    _Phone = Regex.Replace(Phone, @"\s+", " ");
                }
            }
        }


        public string _TaxReference { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{1,15}$", ErrorMessage = "Tax reference must be alphanumeric")]
        [MaxLength(15, ErrorMessage = "Tax reference max length upto 15")]
        public string TaxReference
        {
            get { return _TaxReference; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _TaxReference = value.Trim();
                    _TaxReference = Regex.Replace(TaxReference, @"\s+", " ");
                }
            }
        }


        public string _CompanyRegNumber { get; set; }

        //[RegularExpression(@"^[0-9]{1,15}$", ErrorMessage = "Company registered number should be digits. Max length upto 15.")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,15}$", ErrorMessage = "Company registered number must be alphanumeric")]
        [MaxLength(15, ErrorMessage = "Company registered number max length upto 15")]
        public string CompanyRegNumber
        {
            get { return _CompanyRegNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _CompanyRegNumber = value.Trim();
                    _CompanyRegNumber = Regex.Replace(CompanyRegNumber, @"\s+", " ");
                }
            }
        }



        public string _CompanyRegAddress { get; set; }

        [MaxLength(150, ErrorMessage = "Company registered address max length upto 150")]
        public string CompanyRegAddress
        {
            get { return _CompanyRegAddress; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _CompanyRegAddress = value.Trim();
                    _CompanyRegAddress = Regex.Replace(CompanyRegAddress, @"\s+", " ");
                }
            }
        }



        public string _VatNumber { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{1,15}$", ErrorMessage = "VAT number must be alphanumeric and max length upto 15")]
        public string VatNumber
        {
            get { return _VatNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _VatNumber = value.Trim();
                    _VatNumber = Regex.Replace(VatNumber, @"\s+", " ");
                }
            }
        }



        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string LogoUrl { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public Nullable<bool> IsInvoiced { get; set; }


    }
}