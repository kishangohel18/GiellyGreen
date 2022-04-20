using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenApi.Models
{
    public class CustomHeaderViewModel
    {
         public int Id { get; set; }
        public string InvoiceReferance { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public Nullable<int> CurrentMonth { get; set; }
        public Nullable<int> CurrentYear { get; set; }
    }
}