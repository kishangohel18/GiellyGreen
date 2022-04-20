using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccessLayer.Model;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;

namespace GiellyGreenApi.Controllers
{

    [Authorize]
    public class Monthly_InvoiceController : ApiController
    {
        //private GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();


        [Route("GetAllSupplierByIsActive")]
        public JsonResponse GetAllSupplierByIsActive(string month, string year)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var ObjSupplierList = ObjDataAccess.GetAllSupplierByIsActive(Convert.ToInt32(month), Convert.ToInt32(year)).ToList();

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


        [Route("GetCustomHeader")]
        public JsonResponse InsetUpdateMonthly_Invoice(List<MonthlyInvoiceViewModel> ListOfSupplier)
        {
            var ObjResponse = new JsonResponse();
            try
            {

                if (ListOfSupplier != null && ListOfSupplier.Count > 0)
                {
                    foreach (var Item in ListOfSupplier)
                    {
                        if (Item.MonthlyInvoiceId == 0)
                        {
                            var ObjSupplierList = ObjDataAccess.InsetUpdateMonthly_Invoice(0, Item.SupplierId, Item.SupplierName, Item.HairService, Item.BeautyService, Item.Custom1, Item.Custom2, Item.Custom3, Item.Custom4, Item.Custom5, Item.Net, Item.Vat, Item.Gross, Item.AdvancePaid, Item.Balance, Item.InvoiceReference, Item.IsApproved, Item.InvoiceDate, Item.CurrentYear, Item.CurrentMonth, Item.IsSelected).ToList();
                        }
                        else
                        {
                            var ObjSupplierList = ObjDataAccess.InsetUpdateMonthly_Invoice(Item.MonthlyInvoiceId, Item.SupplierId, Item.SupplierName, Item.HairService, Item.BeautyService, Item.Custom1, Item.Custom2, Item.Custom3, Item.Custom4, Item.Custom5, Item.Net, Item.Vat, Item.Gross, Item.AdvancePaid, Item.Balance, Item.InvoiceReference, Item.IsApproved, Item.InvoiceDate, Item.CurrentYear, Item.CurrentMonth, Item.IsSelected).ToList();
                        }
                    }
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record saved.", null);
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
            }

            return ObjResponse;
        }


        [Route("GetCustomHeader")]
        public JsonResponse GetCustomHeader()
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var ObjSupplierList = ObjDataAccess.GetAllCustom_Header().ToList();

                if (ObjSupplierList != null && ObjSupplierList.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Custom record found.", ObjSupplierList);
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


        [Route("GetCustomHeaderByDate")]
        public JsonResponse GetCustomHeaderByDate(string month, string year)
        {
            var ObjResponse = new JsonResponse();
            try
            {

                var ObjSupplierList = ObjDataAccess.GetCustomHeaderByDate(Convert.ToInt32(month), Convert.ToInt32(year)).ToList();

                if (ObjSupplierList != null && ObjSupplierList.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Custom record found.", ObjSupplierList);
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


        [Route("InsertUpdateCustomHeader")]
        public JsonResponse InsertUpdateCustomHeader(CustomHeaderViewModel model)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        var ObjSupplierList = ObjDataAccess.InsertUpdateCustomHeader(0, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.CurrentMonth, model.CurrentYear).FirstOrDefault();
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record created.", null);
                    }
                    else
                    {
                        var ObjSupplierList = ObjDataAccess.InsertUpdateCustomHeader(model.Id, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.CurrentMonth, model.CurrentYear).FirstOrDefault();
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record " + ObjSupplierList.CustomHeaderId + " updated.", null);
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
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
            }

            return ObjResponse;
        }



        //[Route("ApproveStatus")]
        //public JsonResponse ApproveStatus(List<int> id)
        //{
        //    var ObjResponse = new JsonResponse();
        //    try
        //    {
        //        if (id.Count > 0)
        //        {
        //            foreach (int i in id)
        //            {
        //                if (i > 0)
        //                {
        //                    //var UpdateApproveStatus = ObjDataAccess.UpdateApproveStatus(i);
        //                }
        //            }
        //            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", id);
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