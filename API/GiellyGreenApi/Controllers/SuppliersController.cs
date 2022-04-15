using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using DataAccessLayer.Model;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;


namespace GiellyGreenApi.Controllers
{
    [Authorize]
    public class SuppliersController : ApiController
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        private GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();

        public JsonResponse Get()
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var ObjSupplierList = ObjDataAccess.GetAllSupplier().ToList();

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
            var ObjResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (ObjDataAccess.Suppliers.Any(s => s.ReferenceNumber == model.ReferenceNumber))
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Reference Number should be unique", null);
                    }
                    else if (ObjDataAccess.Suppliers.Any(s => s.Email == model.Email))
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", null);
                    }
                    else if (ObjDataAccess.Suppliers.Any(s => s.VatNumber == model.VatNumber))
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Vat number should be unique", null);
                    }
                    else if (ObjDataAccess.Suppliers.Any(s => s.TaxReference == model.TaxReference))
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Tax reference should be unique", null);
                    }
                    else
                    {
                        var ObjProd = ObjDataAccess.InsertUpdateSupplier(0, model.SupplierName, model.ReferenceNumber, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.CreatedDate, model.ModifiedDate, model.LogoUrl, model.IsActive).FirstOrDefault();
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
            var ObjResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    var ObjProd = ObjDataAccess.InsertUpdateSupplier(id, model.SupplierName, model.ReferenceNumber, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.CreatedDate, model.ModifiedDate, model.LogoUrl, model.IsActive).FirstOrDefault();
                    if (ObjDataAccess.Suppliers.Find(id) == null)
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record not found.", null);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", ObjDataAccess.Suppliers.Find(id));
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

        public JsonResponse Patch(int id, IsActiveUpdateModel model)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    var ObjProd = ObjDataAccess.UpdateByIsActive(id, model.IsActive);
                    if (ObjDataAccess.Suppliers.Find(id) == null)
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record not found.", null);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", ObjDataAccess.Suppliers.Find(id));
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

        public JsonResponse Delete(int id)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var DeletedSupplier = ObjDataAccess.Suppliers.Find(id);
                var ObjSupplier = ObjDataAccess.DeleteConstrainedSupplier(id);
                ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record deleted.", DeletedSupplier);
                //ObjSupplier.
                //if (ObjSupplier.ResponseStatus == 0)
                //{
                //    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Record not found.", null);
                //}
                //else if (ObjSupplier.ResponseStatus == 1)
                //{
                //    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record deleted", null);
                //}
                //else if (ObjSupplier.ResponseStatus == 2)
                //{
                //    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Record not delete. Because invoice present for this supplier.", null);
                //}
                //else
                //{
                //    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                //    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error", allErrors);
                //}
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }

            return ObjResponse;
        }





        // GetAllSupplierByIsActive API
        //public JsonResponse Get()
        //{
        //    var ObjResponse = new JsonResponse();
        //    try
        //    {
        //        var ObjSupplierList = ObjDataAccess.GetAllSupplierByIsActive().ToList();

        //        if (ObjSupplierList != null && ObjSupplierList.Count > 0)
        //        {
        //            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Total " + ObjSupplierList.Count + " records found.", ObjSupplierList);
        //        }
        //        else
        //        {
        //            ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "No record found.", null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
        //    }

        //    return ObjResponse;
        //}

    }
}