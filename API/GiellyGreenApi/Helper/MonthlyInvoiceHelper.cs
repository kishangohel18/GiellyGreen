using DataAccessLayer.Model;
using GiellyGreenApi.Controllers;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Routing;

namespace GiellyGreenApi.Helper
{
    public class MonthlyInvoiceHelper
    {
        public static GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();

        public static JsonResponse SendMailWithPDF(int CurrentID)
        {
            var ObjResponse = new JsonResponse();
            var InvoiceInfo = db.GetInvoiceInfoById(CurrentID).FirstOrDefault();
            var SupplierInfo = db.GetSupplierInfoById(InvoiceInfo.SupplierId).FirstOrDefault();
            var MonthInfo = db.GetHeaderInfoById(InvoiceInfo.MonthHeaderId).FirstOrDefault();
            var GetProfileData = db.GetCompanyProfile().FirstOrDefault();

            CombineSupplierInvoice combineSupplierInvoice = new CombineSupplierInvoice
            {
                Supplier = SupplierInfo,
                Invoice = InvoiceInfo,
                Month_Header = MonthInfo,
                Profile = GetProfileData
            };

            string ToEmail = SupplierInfo.Email;
            string MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(MonthInfo.InvoiceMonth));

            string Subj = "Your invoice for the " + MonthName + " " + MonthInfo.InvoiceYear;
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
            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Email sent successfully.", null);

            return ObjResponse;
        }


        public static JsonResponse CombinePDF(int[] ListOfId)
        {
            var ObjResponse = new JsonResponse();
            List<CombineSupplierInvoice> AllSupplierDetail = new List<CombineSupplierInvoice>();

            if (ListOfId.Length > 0)
            {
                for (int i = 0; i < ListOfId.Length; i++)
                {
                    if (ListOfId[i] > 0)
                    {
                        int CurrentId = ListOfId[i];
                        var InvoiceInfo = db.GetInvoiceInfoById(CurrentId).FirstOrDefault();

                        if (InvoiceInfo != null)
                        {
                            var SupplierInfo = db.GetSupplierInfoById(InvoiceInfo.SupplierId).FirstOrDefault();
                            var MonthInfo = db.GetHeaderInfoById(InvoiceInfo.MonthHeaderId).FirstOrDefault();
                            var GetProfileData = db.GetCompanyProfile().FirstOrDefault();

                            CombineSupplierInvoice combineSupplierInvoice = new CombineSupplierInvoice
                            {
                                Supplier = SupplierInfo,
                                Invoice = InvoiceInfo,
                                Month_Header = MonthInfo,
                                Profile = GetProfileData
                            };
                            if (InvoiceInfo.Net != null && InvoiceInfo.Net > 0)
                            {
                                AllSupplierDetail.Add(combineSupplierInvoice);
                            }
                        }
                    }
                }

                PDFController pdfController = new PDFController();
                RouteData route = new RouteData();
                route.Values.Add("action", "CombinePDF");
                route.Values.Add("controller", "PDF");
                System.Web.Mvc.ControllerContext newContext = new
                System.Web.Mvc.ControllerContext(new HttpContextWrapper(HttpContext.Current), route, pdfController);
                pdfController.ControllerContext = newContext;
                string PdfBase64String = pdfController.CombinePDF(AllSupplierDetail);
                if (AllSupplierDetail.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Combined PDF sent successfully.", PdfBase64String);
                }
                else
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Combined PDF cannot generated.", null);
                }
            }
            else
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "No record found.", null);
            }
            return ObjResponse;
        }

        public static dynamic GetInvoiceData(int invoiceId)
        {
            var GetInvoiceData = db.Invoices.Where(i => i.Id == invoiceId).FirstOrDefault();
            if (GetInvoiceData != null)
            {
                var GetNet = GetInvoiceData.Net;
                return GetNet;
            }
            else
            {
                return 0;
            }
        }

        public static Month_HeaderViewModel SetValueToCustom(Month_HeaderViewModel model)
        {
            if (string.IsNullOrEmpty(model.Custom1))
            {
                model.Custom1 = "Custom_1";
            }
            if (string.IsNullOrEmpty(model.Custom2))
            {
                model.Custom2 = "Custom_2";
            }
            if (string.IsNullOrEmpty(model.Custom3))
            {
                model.Custom3 = "Custom_3";
            }
            if (string.IsNullOrEmpty(model.Custom4))
            {
                model.Custom4 = "Custom_4";
            }
            if (string.IsNullOrEmpty(model.Custom5))
            {
                model.Custom5 = "Custom_5";
            }

            return model;
        }
    }
}