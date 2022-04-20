using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using DataAccessLayer.Model;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;

namespace GiellyGreenApi.Controllers
{

    [Authorize]
    public class Monthly_InvoiceController : ApiController
    {
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


        [Route("InsetUpdateMonthly_Invoice")]
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
                            var UpdateApproveStatus = ObjDataAccess.ApproveSelectedInvoice(ListOfId[i]);
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

        [Route("PDF")]
        public JsonResponse PDF()
        {
            var ObjResponse = new JsonResponse();
            try
            {

                var file = @"D:\PrintAllEmployee1.pdf";

                var PDFFile = Convert.ToBase64String(File.ReadAllBytes(file));
                ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", PDFFile);

                string PDFName = Guid.NewGuid().ToString("N") + ".pdf";

                using (FileStream stream = File.Create("D:\\All_PDF\\" + PDFName))
                {
                    Byte[] byteArray = Convert.FromBase64String(PDFFile);
                    stream.Write(byteArray, 0, byteArray.Length);
                }


                //if (ListOfId.Length > 0)
                //{
                //    for (int i = 0; i < ListOfId.Length; i++)
                //    {
                //        if (ListOfId[i] > 0)
                //        {
                //            var file = @"D:\PrintAllEmployee1.pdf";

                //            //var UpdateApproveStatus = ObjDataAccess.ApproveSelectedInvoice(ListOfId[i]);
                //            var PDFFile = Convert.ToBase64String(File.ReadAllBytes(file));

                //        }
                //    }
                //    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", ListOfId);
                //}
                //else
                //{
                //    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "No record found.", null);
                //}
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
            }

            return ObjResponse;
        }








        [Route("SendEmail")]
        public JsonResponse SendEmail(int[] SupplierId)
        {
            var ObjResponse = new JsonResponse();
            EmailController emailController = new EmailController();
            try
            {
                for (int i = 0; i < SupplierId.Length; i++)
                {
                    if (SupplierId[i] > 0)
                    {
                        var GetSupplierEmail = ObjDataAccess.GetSupplierEmailById(SupplierId[i]).FirstOrDefault();
                        string Email = GetSupplierEmail.EMAIL;
                    }




                    //var actionPDF = new Rotativa.ViewAsPdf("Supplier");
                    //byte[] applicationPDFData = actionPDF.BuildPdf(ControllerContext);


                    //var getMonthlyInvoice = ObjDataAccess.Monthly_Invoice.Where(s => s.SupplierId == getSupplier.SupplierId).FirstOrDefault();

                    //emailController.Email_Attachment(getEmail, "Your invoice for the <month>, <year>", "");
                    //EmailList[i] = getEmail;
                }


            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
            }

            return ObjResponse;
        }


    }
}