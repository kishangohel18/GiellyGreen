using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class InvoiceRepository : IInvoice
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        public List<GetInvoiceByDate_Result> GetInvoiceByDate(int month, int year)
        {
            var InvoicesList = ObjDataAccess.GetInvoiceByDate(month, year).ToList();
            return InvoicesList;
        }

        public List<GetHeaderByDate_Result> GetHeaderByDate(int month, int year)
        {
            var HeaderList = ObjDataAccess.GetHeaderByDate(month, year).ToList();
            return HeaderList;
        }

        public dynamic InsertUpdateInvoice(Invoice Item)
        {
            var ObjSupplierList = ObjDataAccess.InsetUpdateInvoices(Item.Id, Item.MonthHeaderId, Item.SupplierId, Item.SupplierName, Item.HairService, Item.BeautyService, Item.Custom1, Item.Custom2, Item.Custom3, Item.Custom4, Item.Custom5, Item.Net, Item.Vat, Item.Gross, Item.AdvancePaid, Item.Balance, Item.IsApproved).ToList();
            return ObjSupplierList;
        }
        

    }
}
