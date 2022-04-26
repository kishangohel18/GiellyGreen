﻿using DataAccessLayer.Interface;
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

        public dynamic InsertUpdateHeader(Month_Header model)
        {
            dynamic[] Response = new dynamic[3];
            if (model.Id == 0)
            {
                if (ObjDataAccess.Month_Header.Any(d => d.InvoiceMonth != model.InvoiceMonth && d.InvoiceYear != model.InvoiceYear))
                {
                    var ObjSupplierList = ObjDataAccess.InsertUpdateMonthHeader(0, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate).FirstOrDefault();
                    Response[0] = 1;
                    Response[1] = "Record created.";
                    Response[2] = model;
                }
                else
                {
                    Response[0] = 0;
                    Response[1] = "This record has same invoice month.";
                    Response[2] = null;
                }
            }
            else
            {
                var ObjSupplierList = ObjDataAccess.InsertUpdateMonthHeader(model.Id, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate).FirstOrDefault();
                Response[0] = 1;
                Response[1] = "Record " + ObjSupplierList.MonthHeader + " updated.";
                Response[2] = model;
            }

            return Response;
        }

        public void ApproveSelectedInvoice(int invoiceid)
        {
            ObjDataAccess.ApproveSelectedInvoice(invoiceid);
        }
       
    }
}