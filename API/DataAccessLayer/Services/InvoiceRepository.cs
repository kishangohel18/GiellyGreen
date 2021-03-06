using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Linq;


namespace DataAccessLayer.Services
{
    public class InvoiceRepository : IInvoice
    {

        public GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();

        public List<GetInvoiceByDate_Result> GetInvoiceByDate(int month, int year)
        {
            return db.GetInvoiceByDate(month, year).ToList();
        }

        public List<GetHeaderByDate_Result> GetHeaderByDate(int month, int year)
        {
            return db.GetHeaderByDate(month, year).ToList();
        }

        public void InsertUpdateInvoice(Invoice Item)
        {
            if (Item != null)
            {
                if (Item.Net != null && Item.Net > 0)
                {
                   db.InsetUpdateInvoices(Item.Id, Item.MonthHeaderId, Item.SupplierId, Item.SupplierName, Item.HairService, Item.BeautyService, Item.Custom1, Item.Custom2, Item.Custom3, Item.Custom4, Item.Custom5, Item.Net, Item.Vat, Item.Gross, Item.AdvancePaid, Item.Balance, Item.IsApproved).ToList();
                }
            }

        }

        public dynamic InsertUpdateHeader(Month_Header model)
        {
            dynamic[] Response = new dynamic[3];
            if (model.Id == 0)
            {
                var objMonthData = db.Month_Header.Where(m => m.InvoiceMonth == model.InvoiceMonth && m.InvoiceYear == model.InvoiceYear).FirstOrDefault();
                if (objMonthData == null)
                {
                    var ObjSupplierList = db.InsertUpdateMonthHeader(0, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate, model.VatPercentage).FirstOrDefault();

                    Response[0] = 1;
                    Response[1] = "Record created.";
                    Response[2] = ObjSupplierList.MonthHeader;
                }
                else
                {
                    Response[0] = 0;
                    Response[1] = "This record has data for same month.";
                    Response[2] = model;
                }
            }
            else
            {
                var ObjSupplierList = db.InsertUpdateMonthHeader(model.Id, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate, model.VatPercentage).FirstOrDefault();
                Response[0] = 1;
                Response[1] = "Record " + ObjSupplierList.MonthHeader + " updated.";
                Response[2] = model.Id;
            }

            return Response;
        }

        public int ApproveSelectedInvoice(int invoiceid)
        {
            int ResponseApprove;
            var objSupplierData = db.Invoices.Where(s => s.Id == invoiceid).FirstOrDefault();

            if (objSupplierData.Net > 0 && objSupplierData.Net != null) 
            {
                db.ApproveSelectedInvoice(invoiceid);
                ResponseApprove = 1;
            }
            else
            {
                ResponseApprove = 0;
            }
            return ResponseApprove;
        }

        public List<GetInvoicesByMonth_Result> GetInvoicesByMonth(int month, int year)
        {
            return db.GetInvoicesByMonth(month, year).ToList();
        }
    }
}
