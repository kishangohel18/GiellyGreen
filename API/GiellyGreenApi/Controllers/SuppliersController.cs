using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using DataAccessLayer.Model;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;


namespace GiellyGreenApi.Controllers
{

    [Authorize]
    public class SuppliersController : ApiController
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();
        public static JsonResponse ObjResponse = new JsonResponse();

        public JsonResponse Get()
        {
            try
            {
                var ObjSupplierList = ObjDataAccess.GetAllSupplier(0).ToList();

                ObjSupplierList.ForEach(supplier =>
                {
                    string path = HttpContext.Current.Server.MapPath("~/ImageStorage");

                    if (!string.IsNullOrEmpty(supplier.LogoUrl) && supplier.LogoUrl != "null")
                    {
                        string imgPath = Path.Combine(path, supplier.LogoUrl);
                        byte[] imageByte = File.ReadAllBytes(imgPath);
                        supplier.LogoUrl = Convert.ToBase64String(imageByte);
                    }
                });

                if (ObjSupplierList != null && ObjSupplierList.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Total " + ObjSupplierList.Count + " records found.", ObjSupplierList);
                }
                else
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "No record found.", null);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
            }

            return ObjResponse;
        }

        public JsonResponse Post(SupplierViewModel model)
        {
            try
            {             
                if (ModelState.IsValid)
                {                   
                    model.LogoUrl = SupplierHelper.setLogo(model.SupplierName.Trim(), model.SupplierReference.Trim(), model.LogoUrl.Trim());

                    ObjResponse = SupplierHelper.CheckDuplicate(model.SupplierId, model);
                    if (ObjResponse.ResponseStatus != 0)
                    {
                        model = SupplierHelper.RemoveExtraSpace(model);
                        var ObjProd = ObjDataAccess.InsertUpdateSupplier(0, model.SupplierName?.Trim(), model.SupplierReference?.Trim(), model.BusinessAddress?.Trim(), model.Email?.ToLower().Trim(), model.Phone?.Trim(), model.TaxReference?.Trim(), model.CompanyRegNumber?.Trim(), model.CompanyRegAddress?.Trim(), model.VatNumber?.Trim(), model.LogoUrl, model.IsActive).FirstOrDefault();
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record created.", ObjDataAccess.Suppliers.Find(ObjProd.Id));
                    }
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }
            return ObjResponse;
        }

        public JsonResponse Put(int id, SupplierViewModel model)
        {
            SupplierHelper.TrimWhiteSpaceOnRequest(model);
            try
            {
                if (ModelState.IsValid)
                {
                    model.LogoUrl = SupplierHelper.setLogo(model.SupplierName?.Trim(), model.SupplierReference?.Trim(), model.LogoUrl.Trim());
                    ObjResponse = SupplierHelper.CheckDuplicate(id, model);
                    if (ObjResponse.ResponseStatus != 0)
                    {
                        var ObjProd = ObjDataAccess.InsertUpdateSupplier(id, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.LogoUrl, model.IsActive).FirstOrDefault();

                        if (ObjDataAccess.Suppliers.Find(id) == null)
                        {
                            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record not found.", null);
                        }
                        else
                        {
                            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", model);
                        }
                    }
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }
            return ObjResponse;
        }

        [Route("ToggleActive")]
        public JsonResponse ToggleActive(int id, bool IsActive)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ObjProd = ObjDataAccess.UpdateStatus(id, IsActive);
                    if (ObjDataAccess.Suppliers.Find(id) == null)
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Record not found.", null);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", ObjDataAccess.Suppliers.Find(id));
                    }
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }
            return ObjResponse;
        }

        public JsonResponse Delete(int id)
        {
            try
            {
                var CurrentSupplier = ObjDataAccess.Suppliers.Find(id);
                var ObjSupplier = ObjDataAccess.DeleteSupplier(id).FirstOrDefault();

                if (ObjSupplier.ResponseStatus == 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Record not found.", null);
                }
                else if (ObjSupplier.ResponseStatus == 1)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record deleted", CurrentSupplier);
                }
                else if (ObjSupplier.ResponseStatus == 2)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Record cannot delete. Because invoice present for this supplier.", CurrentSupplier);
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Error", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }
            return ObjResponse;
        }

    }
}
