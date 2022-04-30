using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Model;

namespace GiellyGreenApi.Models
{
    public class InvoiceViewModel
    {

        public int Id { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string VatNumber { get; set; }

        [Range(0,1000000,ErrorMessage = "Range between 0 to 1000000 in Hair service.")]
        public Nullable<decimal> HairService { get; set; }

        [Range(0, 1000000, ErrorMessage = "Range between 0 to 1000000 in Beauty service.")]
        public Nullable<decimal> BeautyService { get; set; }

        [Range(0, 1000000, ErrorMessage = "Range between 0 to 1000000.")]
        public Nullable<decimal> Custom1 { get; set; }

        [Range(0, 1000000, ErrorMessage = "Range between 0 to 1000000.")]
        public Nullable<decimal> Custom2 { get; set; }

        [Range(0, 1000000, ErrorMessage = "Range between 0 to 1000000.")]
        public Nullable<decimal> Custom3 { get; set; }

        [Range(0, 1000000, ErrorMessage = "Range between 0 to 1000000.")]
        public Nullable<decimal> Custom4 { get; set; }

        [Range(0, 1000000, ErrorMessage = "Range between 0 to 1000000.")]
        public Nullable<decimal> Custom5 { get; set; }

        public Nullable<decimal> Net { get; set; }
        public Nullable<decimal> Vat { get; set; }
        public Nullable<decimal> Gross { get; set; }
        public Nullable<decimal> AdvancePaid { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<int> MonthHeaderId { get; set; }
    }
}