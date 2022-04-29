using DataAccessLayer.Model;


namespace GiellyGreenApi.Models
{
    public class CombineSupplierInvoice
    {
        public GetSupplierInfoById_Result Supplier { get; set; }

        public GetInvoiceInfoById_Result Invoice { get; set; }

        public GetHeaderInfoById_Result Month_Header { get; set; }

        public GetCompanyProfile_Result Profile { get; set; }
    }
}