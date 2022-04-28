using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web.Mvc;

namespace GiellyGreenApi.Controllers
{
    public class PDFController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        

        public dynamic ViewAsPdf(CombineSupplierInvoice combineSupplierInvoice)
        {
            var actionPDF = new Rotativa.ViewAsPdf("PDFForInvoice", combineSupplierInvoice);
            byte[] applicationPDFData = actionPDF.BuildFile(ControllerContext);
            Attachment att = new Attachment(new MemoryStream(applicationPDFData), "Invoice.pdf");

            return att;
        }

        public ActionResult PDFForInvoice(CombineSupplierInvoice combineSupplierInvoice)
        {
            return View(combineSupplierInvoice);
        }

        public string CombinePDF(List<CombineSupplierInvoice> combineSupplierInvoice)
        {
            var actionPDF = new Rotativa.ViewAsPdf("CombinePDFView", combineSupplierInvoice);
            byte[] applicationPDFData = actionPDF.BuildFile(ControllerContext);
            string Base64StringPDF = Convert.ToBase64String(applicationPDFData);

            return Base64StringPDF;
        }

        public ActionResult CombinePDFView(List<CombineSupplierInvoice> combineSupplierInvoice)
        {
            return View(combineSupplierInvoice);
        }

    }
}