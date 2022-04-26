using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Routing;
using AutoMapper;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using DataAccessLayer.Services;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;

namespace GiellyGreenApi.Controllers
{

    [Authorize]
    public class Monthly_InvoiceController : ApiController
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();
        public static JsonResponse ObjResponse = new JsonResponse();
        private readonly IInvoice InvoiceRepository = new InvoiceRepository();


        [Route("GetInvoiceByDate")]
        public JsonResponse GetInvoiceByDate(int month, int year)
        {
            try
            {
                //var InvoicesList = ObjDataAccess.GetInvoiceByDate(Convert.ToInt32(month), Convert.ToInt32(year)).ToList();
                var InvoicesList = InvoiceRepository.GetInvoiceByDate(month, year);
                //var HeaderList = ObjDataAccess.GetHeaderByDate(Convert.ToInt32(month), Convert.ToInt32(year)).ToList();
                var HeaderList = InvoiceRepository.GetHeaderByDate(month, year);

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

        [Route("InsertUpdateInvoices")]
        public JsonResponse InsertUpdateInvoices(List<InvoiceViewModel> ListOfSupplierInvoice)
        {
            try
            {

                if (ListOfSupplierInvoice != null && ListOfSupplierInvoice.Count > 0)
                {
                    foreach (var Item in ListOfSupplierInvoice)
                    {
                        var config = new MapperConfiguration(cfg =>
                                  cfg.CreateMap<InvoiceViewModel, Invoice>());

                        var mapper = config.CreateMapper();
                        var ObjInvoiceMapper = mapper.Map<Invoice>(Item);
                        //var ObjSupplierList = ObjDataAccess.InsetUpdateInvoices(Item.Id, Item.MonthHeaderId, Item.SupplierId, Item.SupplierName, Item.HairService, Item.BeautyService, Item.Custom1, Item.Custom2, Item.Custom3, Item.Custom4, Item.Custom5, Item.Net, Item.Vat, Item.Gross, Item.AdvancePaid, Item.Balance, Item.IsApproved).ToList();
                        //var ObjSupplierList = InvoiceRepository.InsertUpdateInvoice();
                        InvoiceRepository.InsertUpdateInvoice(ObjInvoiceMapper);

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
        public JsonResponse GetHeaderByDate(int month, int year)
        {
            try
            {
                //var ObjSupplierList = ObjDataAccess.GetHeaderByDate(month, year).ToList();
                var HeaderList = InvoiceRepository.GetHeaderByDate(month, year);

                if (HeaderList != null && HeaderList.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record found.", HeaderList);
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
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Id == 0)
                    {
                        if (ObjDataAccess.Month_Header.Any(d => d.InvoiceMonth != model.InvoiceMonth && d.InvoiceYear != model.InvoiceYear))
                        {
                            var ObjSupplierList = ObjDataAccess.InsertUpdateMonthHeader(0, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate).FirstOrDefault();
                            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record created.", model);
                        }
                        else
                        {
                            ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "This record has same invoice month.", null);
                        }
                    }
                    else
                    {
                        var ObjSupplierList = ObjDataAccess.InsertUpdateMonthHeader(model.Id, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate).FirstOrDefault();
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record " + ObjSupplierList.MonthHeader + " updated.", model);
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