using DataAccessLayer.Model;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
           
            if (ObjDataAccess.Suppliers.Any(s => s.SupplierReference == model.SupplierReference && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Reference Number should be unique", null);
            }
            else if (ObjDataAccess.Suppliers.Any(s => s.Email == model.Email && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", null);
            }
            else if (ObjDataAccess.Suppliers.Any(s => s.VatNumber == model.VatNumber && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Vat number should be unique", null);
            }
            else if (ObjDataAccess.Suppliers.Any(s => s.TaxReference == model.TaxReference && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Tax reference should be unique", null);
            }

            return ObjResponse;
        }

        public static SupplierViewModel RemoveExtraSpace(SupplierViewModel model)
        {
            model.SupplierName = Regex.Replace(model.SupplierName, @"\s+", " ");
            model.SupplierReference = Regex.Replace(model.SupplierReference, @"\s+", " ");
            model.BusinessAddress = Regex.Replace(model.BusinessAddress ?? "", @"\s+", " ");
            model.Email = Regex.Replace(model.Email, @"\s+", " ");
            model.TaxReference = Regex.Replace(model.TaxReference ?? "", @"\s+", " ");
            model.CompanyRegNumber = Regex.Replace(model.CompanyRegNumber ?? "", @"\s+", " ");
            model.CompanyRegAddress = Regex.Replace(model.CompanyRegAddress ?? "", @"\s+", " ");
            model.VatNumber = Regex.Replace(model.VatNumber ?? "", @"\s+", " ");

            return model;
        }
    }
}