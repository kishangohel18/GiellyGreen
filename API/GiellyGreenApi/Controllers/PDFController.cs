using DataAccessLayer.Model;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GiellyGreenApi.Controllers
{
    public class PDFController : Controller
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();


        // GET: PDF
        public ActionResult Index()
        {
            return View();
        }


        //[Route("Supplier")]
        public ActionResult SupplierPDF()
        {
            //var AllSupplierData = ObjDataAccess..ToList();
            return View("~/Views/SupplierPDF.cshtml");
        }

        [Obsolete]
        public dynamic PrintAllEmployee()
        {
            //var actionPDF = new Rotativa.ViewAsPdf("~/Views/SupplierPDF.cshtml");
            var actionPDF = new Rotativa.ActionAsPdf("SupplierPDF");
            byte[] applicationPDFData = actionPDF.BuildPdf(ControllerContext);
            Attachment att = new Attachment(new MemoryStream(applicationPDFData), "Invoice.pdf");



            //var report = new Rotativa.ActionAsPdf("SupplierPDF");
            //return report;
            return att;
        }


        public static CombineSupplierInvoice combineSupplierInvoiceData;

        public dynamic ViewAsPdf(CombineSupplierInvoice combineSupplierInvoice)
        {
            combineSupplierInvoiceData = combineSupplierInvoice;

            var actionPDF = new Rotativa.ViewAsPdf("PDFForInvoice");
            byte[] applicationPDFData = actionPDF.BuildFile(ControllerContext);
            Attachment att = new Attachment(new MemoryStream(applicationPDFData), "Invoice.pdf");

            return att;
        }


        public ActionResult PDFForInvoice(CombineSupplierInvoice combineSupplierInvoice)
        {

            combineSupplierInvoice = combineSupplierInvoiceData;

            return View(combineSupplierInvoice);
        }


    }
}