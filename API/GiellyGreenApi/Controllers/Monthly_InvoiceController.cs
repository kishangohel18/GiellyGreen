using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Routing;
using DataAccessLayer.Model;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;

namespace GiellyGreenApi.Controllers
{

    [Authorize]
    public class Monthly_InvoiceController : ApiController
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();


        [Route("GetInvoiceByDate")]
        public JsonResponse GetInvoiceByDate(string month, string year)
        {
            var ObjResponse = new JsonResponse();
            try
            {

                var InvoicesList = ObjDataAccess.GetInvoiceByDate(Convert.ToInt32(month), Convert.ToInt32(year)).ToList();
                var HeaderList = ObjDataAccess.GetHeaderByDate(Convert.ToInt32(month), Convert.ToInt32(year)).ToList();

                if (InvoicesList != null && InvoicesList.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Total " + InvoicesList.Count + " records found.", new { HeaderList, InvoicesList });
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


        [Route("InsetUpdateInvoices")]
        public JsonResponse InsetUpdateInvoices(List<InvoiceViewModel> ListOfSupplierInvoice)
        {
            var ObjResponse = new JsonResponse();
            try
            {

                if (ListOfSupplierInvoice != null && ListOfSupplierInvoice.Count > 0)
                {
                    foreach (var Item in ListOfSupplierInvoice)
                    {
                        var ObjSupplierList = ObjDataAccess.InsetUpdateInvoices(Item.Id, Item.MonthHeaderId, Item.SupplierId, Item.SupplierName, Item.HairService, Item.BeautyService, Item.Custom1, Item.Custom2, Item.Custom3, Item.Custom4, Item.Custom5, Item.Net, Item.Vat, Item.Gross, Item.AdvancePaid, Item.Balance, Item.IsApproved).ToList();
                    }
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record saved.", ListOfSupplierInvoice);
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


        [Route("GetHeaderByDate")]
        public JsonResponse GetHeaderByDate(string month, string year)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var ObjSupplierList = ObjDataAccess.GetHeaderByDate(Convert.ToInt32(month), Convert.ToInt32(year)).ToList();

                if (ObjSupplierList != null && ObjSupplierList.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record found.", ObjSupplierList);
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


        [Route("InsertUpdateMonthHeader")]
        public JsonResponse InsertUpdateMonthHeader(Month_HeaderViewModel model)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (ObjDataAccess.Month_Header.Any(d => d.InvoiceMonth == model.InvoiceMonth && d.InvoiceYear == model.InvoiceYear))
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "This record has same invoice month.", null);
                    }
                    else
                    {
                        if (model.Id == 0)
                        {
                            var ObjSupplierList = ObjDataAccess.InsertUpdateMonthHeader(0, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate).FirstOrDefault();
                            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record created.", model);
                        }
                        else
                        {
                            var ObjSupplierList = ObjDataAccess.InsertUpdateMonthHeader(model.Id, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate).FirstOrDefault();
                            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record " + ObjSupplierList.MonthHeader + " updated.", model);
                        }
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


        [Route("ApproveSelectedInvoice")]
        public JsonResponse ApproveSelectedInvoice(int[] ListOfId)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (ListOfId.Length > 0)
                {
                    for (int i = 0; i < ListOfId.Length; i++)
                    {
                        if (ListOfId[i] > 0)
                        {
                            int InvoiceId = ListOfId[i];
                            var UpdateApproveStatus = ObjDataAccess.ApproveSelectedInvoice(InvoiceId);
                        }
                    }
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", ListOfId);
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


        [Route("SendEmail")]
        public JsonResponse SendEmail(int[] ListOfId)
        {
            var ObjResponse = new JsonResponse();

            try
            {
                if (ListOfId.Length > 0)
                {
                    for (int i = 0; i < ListOfId.Length; i++)
                    {
                        if (ListOfId[i] > 0)
                        {
                            int CurrentId = ListOfId[i];
                            ObjResponse = MonthlyInvoiceHelper.SendMailWithPDF(CurrentId);
                        }
                    }
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Email sent successfully.", null);
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


        [Route("CombinePDF")]
        public JsonResponse CombinePDF(int[] ListOfId)
        {
            var ObjResponse = new JsonResponse();
            List<CombineSupplierInvoice> AllSupplierDetail = new List<CombineSupplierInvoice>();

            try
            {
                ObjResponse = MonthlyInvoiceHelper.CombinePDF(ListOfId);               
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
            }

            return ObjResponse;
        }

    }
}