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
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<int> MonthHeaderId { get; set; }
    }
}