using DataAccessLayer.Model;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenApi.Helper
{
    public class SupplierHelper
    {
        public static GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        public static JsonResponse CheckDuplicate(int id, SupplierViewModel model)
        {
            var ObjResponse = new JsonResponse();
            ObjResponse.ResponseStatus = 2;

            if (ObjDataAccess.Suppliers.Any(s => s.SupplierReference == model.SupplierReference && s.SupplierId != id) && model.SupplierReference != null && model.SupplierReference != "")
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Reference Number should be unique", null);
            }
            else if (ObjDataAccess.Suppliers.Any(s => s.Email == model.Email && s.SupplierId != id) && model.Email != null && model.Email != "")
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", null);
            }
            else if (ObjDataAccess.Suppliers.Any(s => s.VatNumber == model.VatNumber && s.SupplierId != id) && model.VatNumber != null && model.VatNumber != "")
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Vat number should be unique", null);
            }
            else if (ObjDataAccess.Suppliers.Any(s => s.TaxReference == model.TaxReference && s.SupplierId != id) && model.TaxReference != null && model.TaxReference != "")
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Tax reference should be unique", null);
            }

            return ObjResponse;
        }
    }
}