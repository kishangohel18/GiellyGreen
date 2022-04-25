using DataAccessLayer.Model;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace GiellyGreenApi.Helper
{
    public class SupplierHelper
    {
        public static GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        public static string setLogo(string SupplierName,string SupplierReference,string Base64String)
        {
            string path = HttpContext.Current.Server.MapPath("~/ImageStorage");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!string.IsNullOrEmpty(Base64String))
            {
                string imageName = SupplierName + "_" + SupplierReference + ".jpg";
                string imgPath = Path.Combine(path, imageName);
                byte[] imageBytes = Convert.FromBase64String(Base64String);
                File.WriteAllBytes(imgPath, imageBytes);
                Base64String = imgPath;
            }
            return Base64String;
        }


        public static JsonResponse CheckDuplicate(int id, SupplierViewModel model)
        {
            var ObjResponse = new JsonResponse();
            ObjResponse.ResponseStatus = 2;
            
            if (ObjDataAccess.Suppliers.Any(s => s.SupplierReference == model.SupplierReference && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Supplier reference should be unique", null);
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
            model.SupplierReference = model.SupplierReference?.Trim();
            model.Email = model.Email?.ToLower().Trim();
            model.VatNumber = model.VatNumber?.Trim();
            model.TaxReference = model.TaxReference?.Trim();
            model.SupplierName = model.SupplierName?.Trim();
            model.BusinessAddress = model.BusinessAddress?.Trim();
            model.Phone = model.Phone?.Trim();
            model.CompanyRegNumber = model.CompanyRegNumber?.Trim();
            model.CompanyRegAddress = model.CompanyRegAddress?.Trim();           

            return model;
        }

        public static SupplierViewModel TrimWhiteSpaceOnRequest<SupplierViewModel>(SupplierViewModel model)
        {
            var ObjResponse = new JsonResponse();

            if (model != null)
            {
                PropertyInfo[] properties = model.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    try
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            var o = property.GetValue(model, null) ?? "";
                            string s = (string)o;
                            property.SetValue(model, s.Trim());
                        }
                        else
                        {

                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                }

            }
            return model;
        }
    }
}