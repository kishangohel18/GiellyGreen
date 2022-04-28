using DataAccessLayer.Model;


namespace GiellyGreenApi.Models
{
    public class CombineSupplierInvoice
    {
        public Supplier Supplier { get; set; }

        public Invoice Invoice { get; set; }

        public Month_Header Month_Header { get; set; }

        public GetCompanyProfile_Result Profile { get; set; }
    }
}