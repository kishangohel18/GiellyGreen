using DataAccessLayer.Model;
using GiellyGreenApi.Controllers;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Routing;

namespace GiellyGreenApi.Helper
{
    public class MonthlyInvoiceHelper
    {
        public static GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        public static JsonResponse SendMailWithPDF(int CurrentID)
        {
            var ObjResponse = new JsonResponse();
            var SupplierInfo = ObjDataAccess.Suppliers.Find(CurrentID);
            var InvoiceInfo = ObjDataAccess.Invoices.Where(s => s.SupplierId == CurrentID).FirstOrDefault();
            var MonthInfo = ObjDataAccess.Month_Header.Where(s => s.Id == InvoiceInfo.MonthHeaderId).FirstOrDefault();

            CombineSupplierInvoice combineSupplierInvoice = new CombineSupplierInvoice
            {
                Supplier = SupplierInfo,
                Invoice = InvoiceInfo,
                Month_Header = MonthInfo
            };

            string ToEmail = SupplierInfo.Email;

            string Subj = "Your invoice for the " + MonthInfo.InvoiceMonth + "," + MonthInfo.InvoiceYear;
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
       
    }
}