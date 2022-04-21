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
                var ObjSupplierList = ObjDataAccess.GetInvoiceByDate(Convert.ToInt32(month), Convert.ToInt32(year)).ToList();

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
                        if (Item.Id == 0)
                        {
                            var ObjSupplierList = ObjDataAccess.InsetUpdateInvoices(0, Item.MonthHeaderId, Item.SupplierId, Item.SupplierName, Item.HairService, Item.BeautyService, Item.Custom1, Item.Custom2, Item.Custom3, Item.Custom4, Item.Custom5, Item.Net, Item.Vat, Item.Gross, Item.AdvancePaid, Item.Balance, Item.IsApproved).ToList();
                        }
                        else
                        {
                            var ObjSupplierList = ObjDataAccess.InsetUpdateInvoices(Item.Id, Item.MonthHeaderId, Item.SupplierId, Item.SupplierName, Item.HairService, Item.BeautyService, Item.Custom1, Item.Custom2, Item.Custom3, Item.Custom4, Item.Custom5, Item.Net, Item.Vat, Item.Gross, Item.AdvancePaid, Item.Balance, Item.IsApproved).ToList();
                        }
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
                    if (model.Id == 0)
                    {
                        var ObjSupplierList = ObjDataAccess.InsertUpdateMonthHeader(0, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear,model.InvoiceDate).FirstOrDefault();
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record created.", null);
                    }
                    else
                    {
                        var ObjSupplierList = ObjDataAccess.InsertUpdateMonthHeader(model.Id, model.InvoiceReferance, model.Custom1, model.Custom2, model.Custom3, model.Custom4, model.Custom5, model.InvoiceMonth, model.InvoiceYear, model.InvoiceDate).FirstOrDefault();
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record " + ObjSupplierList.MonthHeader + " updated.", null);
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


        //[Route("PDF")]
        //public JsonResponse PDF()
        //{
        //    var ObjResponse = new JsonResponse();
        //    try
        //    {

        //        var file = @"D:\PrintAllEmployee1.pdf";

        //        var PDFFile = Convert.ToBase64String(File.ReadAllBytes(file));
        //        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", PDFFile);

        //        string PDFName = Guid.NewGuid().ToString("N") + ".pdf";

        //        using (FileStream stream = File.Create("D:\\All_PDF\\" + PDFName))
        //        {
        //            Byte[] byteArray = Convert.FromBase64String(PDFFile);
        //            stream.Write(byteArray, 0, byteArray.Length);
        //        }


        //        //if (ListOfId.Length > 0)
        //        //{
        //        //    for (int i = 0; i < ListOfId.Length; i++)
        //        //    {
        //        //        if (ListOfId[i] > 0)
        //        //        {
        //        //            var file = @"D:\PrintAllEmployee1.pdf";

        //        //            //var UpdateApproveStatus = ObjDataAccess.ApproveSelectedInvoice(ListOfId[i]);
        //        //            var PDFFile = Convert.ToBase64String(File.ReadAllBytes(file));

        //        //        }
        //        //    }
        //        //    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", ListOfId);
        //        //}
        //        //else
        //        //{
        //        //    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "No record found.", null);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
        //    }

        //    return ObjResponse;
        //}


        //[Route("SendEmail")]
        //public JsonResponse SendEmail(int[] SupplierId)
        //{
        //    var ObjResponse = new JsonResponse();
        //    EmailController EmailController = new EmailController();
        //    try
        //    {
        //        for (int i = 0; i < SupplierId.Length; i++)
        //        {
        //            if (SupplierId[i] > 0)
        //            {
        //                var GetSupplierEmail = ObjDataAccess.GetSupplierEmailById(SupplierId[i]).FirstOrDefault();
        //                string Email = GetSupplierEmail.EMAIL;

        //            }


        //            //var actionPDF = new Rotativa.ViewAsPdf("Supplier");
        //            //byte[] applicationPDFData = actionPDF.BuildPdf(ControllerContext);


        //            //var getMonthlyInvoice = ObjDataAccess.Monthly_Invoice.Where(s => s.SupplierId == getSupplier.SupplierId).FirstOrDefault();

        //            //emailController.Email_Attachment(getEmail, "Your invoice for the <month>, <year>", "");
        //            //EmailList[i] = getEmail;
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
        //    }

        //    return ObjResponse;
        //}



        [Route("SendEmail")]
        public JsonResponse SendEmail(int SupplierId)
        {
            var ObjResponse = new JsonResponse();
            //EmailController EmailController = new EmailController();
            try
            {

                var SupplierInfo = ObjDataAccess.Suppliers.Find(SupplierId);
                
                var InvoiceInfo = ObjDataAccess.Invoices.Where(s => s.SupplierId == SupplierId).FirstOrDefault();   

                var MonthInfo = ObjDataAccess.Month_Header.Where(s => s.Id == InvoiceInfo.MonthHeaderId).FirstOrDefault();

                CombineSupplierInvoice combineSupplierInvoice = new CombineSupplierInvoice
                {
                    Supplier = SupplierInfo,
                    Invoice = InvoiceInfo,
                    Month_Header = MonthInfo
                };


                //Redirect()
                string ToEmail = SupplierInfo.Email;

                string Subj = "Your invoice for the 04,2022";
                string Message = "Please find attached a self-billed invoice to Gielly Green Limited, prepared on your behalf, as per the agreement.Regard Gielly Green Limited";              


                var HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
                var FromEmailid = ConfigurationManager.AppSettings["FromEmail"].ToString();
                var Pass = ConfigurationManager.AppSettings["PasswordEmail"].ToString();

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(FromEmailid);
                mailMessage.Subject = Subj;
                mailMessage.Body = Message;
                mailMessage.Body = Message;
                mailMessage.IsBodyHtml = true;

                PDFController pdfController = new PDFController();
                RouteData route = new RouteData();
                route.Values.Add("action", "ViewAsPdf"); 
                route.Values.Add("controller", "PDF");  
                System.Web.Mvc.ControllerContext newContext = new
                System.Web.Mvc.ControllerContext(new HttpContextWrapper(HttpContext.Current), route, pdfController);
                pdfController.ControllerContext = newContext;
                dynamic pdf = pdfController.ViewAsPdf(combineSupplierInvoice);
                mailMessage.Attachments.Add(pdf);


                string[] Multi = ToEmail.Split(',');
                foreach (string Multiemailid in Multi)
                {
                    mailMessage.To.Add(new MailAddress(Multiemailid));
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAdd;

                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mailMessage.From.Address;
                NetworkCred.Password = Pass;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mailMessage);


            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
            }

            return ObjResponse;
        }



    }
}