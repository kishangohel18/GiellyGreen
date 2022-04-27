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
        public static JsonResponse ObjResponse = new JsonResponse();
        private readonly IInvoice InvoiceRepository = new InvoiceRepository();
        private readonly ISupplier SupplierRepository = new SupplierRepository();


        [Route("GetInvoiceByDate")]
        public JsonResponse GetInvoiceByDate(int month, int year)
        {
            try
            {
                var HeaderList = InvoiceRepository.GetHeaderByDate(month, year);
                var InvoicesList = InvoiceRepository.GetInvoiceByDate(month, year);
                var ActiveSupplier = SupplierRepository.ActiveSupplier();
                if (HeaderList.Count == 0)
                {
                    var config = new MapperConfiguration(cfg =>
                                  cfg.CreateMap<GetActiveSupplier_Result, GetInvoiceByDate_Result>());

                    var mapper = config.CreateMapper();
                    InvoicesList.Clear();
                    foreach (var supplier in ActiveSupplier)
                    {
                        var ObjInvoiceMapper = mapper.Map<GetInvoiceByDate_Result>(supplier);
                        InvoicesList.Add(ObjInvoiceMapper);
                    }
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Total " + InvoicesList.Count + " records found.", new { HeaderList, InvoicesList });
                }
                else
                {
                    if (InvoicesList != null && InvoicesList.Count > 0)
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Total " + InvoicesList.Count + " records found.", new { HeaderList, InvoicesList });
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "No record found.", null);
                    }
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
                    var config = new MapperConfiguration(cfg =>
                                    cfg.CreateMap<Month_HeaderViewModel, Month_Header>());
                    var mapper = config.CreateMapper();
                    var ObjInvoiceMapper = mapper.Map<Month_Header>(model);

                    var Response = InvoiceRepository.InsertUpdateHeader(ObjInvoiceMapper);                   
                    ObjResponse.ResponseStatus = Response[0];
                    ObjResponse.Message = Response[1];
                    ObjResponse.Result = Response[2];
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
                            InvoiceRepository.ApproveSelectedInvoice(InvoiceId);
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