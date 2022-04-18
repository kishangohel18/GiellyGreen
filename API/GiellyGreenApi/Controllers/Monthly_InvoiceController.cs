using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccessLayer.Model;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;

namespace GiellyGreenApi.Controllers
{

    [Authorize]
    public class Monthly_InvoiceController : ApiController
    {
        private GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();



        public JsonResponse Get()
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var date = Convert.ToDateTime("18-04-2022");

                var ObjSupplierList = ObjDataAccess.GetInvoiceByMonth(date).ToList();

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



        // GET: api/Monthly_Invoice
        //public IQueryable<Monthly_Invoice> GetMonthly_Invoice()
        //{
        //    return db.Monthly_Invoice;
        //}

        // GET: api/Monthly_Invoice/5
        //[ResponseType(typeof(Monthly_Invoice))]
        //public IHttpActionResult GetMonthly_Invoice(int id)
        //{
        //    Monthly_Invoice monthly_Invoice = db.Monthly_Invoice.Find(id);
        //    if (monthly_Invoice == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(monthly_Invoice);
        //}

        // PUT: api/Monthly_Invoice/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutMonthly_Invoice(int id, Monthly_Invoice monthly_Invoice)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != monthly_Invoice.MonthlyInvoiceId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(monthly_Invoice).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!Monthly_InvoiceExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Monthly_Invoice
        //[ResponseType(typeof(Monthly_Invoice))]
        //public IHttpActionResult PostMonthly_Invoice(Monthly_Invoice monthly_Invoice)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Monthly_Invoice.Add(monthly_Invoice);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = monthly_Invoice.MonthlyInvoiceId }, monthly_Invoice);
        //}

        // DELETE: api/Monthly_Invoice/5
        //[ResponseType(typeof(Monthly_Invoice))]
        //public IHttpActionResult DeleteMonthly_Invoice(int id)
        //{
        //    Monthly_Invoice monthly_Invoice = db.Monthly_Invoice.Find(id);
        //    if (monthly_Invoice == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Monthly_Invoice.Remove(monthly_Invoice);
        //    db.SaveChanges();

        //    return Ok(monthly_Invoice);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool Monthly_InvoiceExists(int id)
        //{
        //    return db.Monthly_Invoice.Count(e => e.MonthlyInvoiceId == id) > 0;
        //}
    }
}