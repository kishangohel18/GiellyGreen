using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Supplier()
        {
            var AllSupplierData = ObjDataAccess.Suppliers.ToList();
            return View(AllSupplierData);
        }

        public ActionResult PrintAllEmployee()
        {
           
            var report = new Rotativa.ActionAsPdf("Supplier");
            return report;
        }

        [Obsolete]
        public void ViewAsPdf()
        {
            var actionPDF = new Rotativa.ViewAsPdf("Supplier");
            byte[] applicationPDFData = actionPDF.BuildPdf(ControllerContext);
        }
    }
}