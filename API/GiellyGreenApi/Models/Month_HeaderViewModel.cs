using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiellyGreenApi.Models
{
    public class Month_HeaderViewModel
    {
        public int Id { get; set; }
        public string InvoiceReferance { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }

        [Range(1,12,ErrorMessage = "Month cannot be greater than 12.")]
        public Nullable<int> InvoiceMonth { get; set; }

        [Range(1000,9999,ErrorMessage = "Year must between 1000 9999.")]
        public Nullable<int> InvoiceYear { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> VatPercentage { get; set; }

    }
}