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
        public static GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();

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
            
            if (db.Suppliers.Any(s => s.SupplierReference == model.SupplierReference && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Supplier reference should be unique", model.SupplierReference);
            }
            else if (db.Suppliers.Any(s => s.Email == model.Email && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", model.Email);
            }
            else if (db.Suppliers.Any(s => s.VatNumber == model.VatNumber && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Vat number should be unique", model.VatNumber);
            }
            else if (db.Suppliers.Any(s => s.TaxReference == model.TaxReference && s.SupplierId != id))
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Tax reference should be unique", model.TaxReference);
            }

            return ObjResponse;
        }       

        public static SupplierViewModel TrimWhiteSpaceOnRequest<SupplierViewModel>(SupplierViewModel model)
        {
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