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
        List<GetHeaderByDate_Result> GetHeaderByDate(int month, int year);
        dynamic InsertUpdateInvoice(Invoice Item);

    }
}
