using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;


namespace DataAccessLayer.Interface
{
    public interface IInvoice
    {
        List<GetInvoiceByDate_Result> GetInvoiceByDate(int month, int year);

        List<GetInvoicesByMonth_Result> GetInvoicesByMonth(int month, int year);

        List<GetHeaderByDate_Result> GetHeaderByDate(int month, int year);
        void InsertUpdateInvoice(Invoice Item);
        dynamic InsertUpdateHeader(Month_Header model);
        int ApproveSelectedInvoice(int invoiceid);
    }
}
