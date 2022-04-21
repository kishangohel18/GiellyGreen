using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenApi.Models
{
    public class CombineSupplierInvoice
    {
        public SupplierViewModel SupplierViewModel { get; set; }
        public InvoiceViewModel InvoiceViewModel { get; set; }
        public Month_HeaderViewModel Month_HeaderViewModel { get; set; }

    }
}