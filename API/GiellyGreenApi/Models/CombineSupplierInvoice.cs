using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace GiellyGreenApi.Models
{
    public class CombineSupplierInvoice
    {
        public Supplier Supplier { get; set; }
        public Invoice Invoice { get; set; }
        public Month_Header Month_Header { get; set; }

    }
}