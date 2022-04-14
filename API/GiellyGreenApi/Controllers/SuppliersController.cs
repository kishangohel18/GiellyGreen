using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class SuppliersController : ApiController
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        private GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();

        // GET: api/Suppliers
        //public IQueryable<Supplier> GetSuppliers()
        //{
        //    return db.Suppliers;
        //}


        public JsonResponse Get()
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var ObjSupplierList = ObjDataAccess.GetAllSupplier().ToList();

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

        // GetAllSupplierByIsActive API
        //public JsonResponse Get()
        //{
        //    var ObjResponse = new JsonResponse();
        //    try
        //    {
        //        var ObjSupplierList = ObjDataAccess.GetAllSupplierByIsActive().ToList();

        //        if (ObjSupplierList != null && ObjSupplierList.Count > 0)
        //        {
        //            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Total " + ObjSupplierList.Count + " records found.", ObjSupplierList);
        //        }
        //        else
        //        {
        //            ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "No record found.", null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
        //    }

        //    return ObjResponse;
        //}

        //public JsonResponse Post(SupplierViewModel model)
        //{
        //    var ObjResponse = new JsonResponse();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var ObjProd = ObjDataAccess.GetAllSupplier(0, model.SupplierName, model.SupplierName, model.ReferenceNumber, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.CreatedDate, model.ModifiedDate, model.Logo, model.IsActive).FirstOrDefault();

        //            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record created.", ObjDataAccess.Products.Find(ObjProd.Response));
        //        }
        //        else
        //        {
        //            var allErrors = ModelState.Values.SelectMany(x => x.Errors);
        //            ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error.", allErrors);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
        //    }

        //    return ObjResponse;
        //}

        //public JsonResponse Put(int id, SupplierViewModel model)
        //{
        //    var ObjResponse = new JsonResponse();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var ObjProd = ObjDataAccess.spKishanInsertUpdate(id, model.Name, model.Description, model.Price, model.OnHandQuantity, model.Status, User.Identity.GetUserId()).FirstOrDefault();
        //            if (ObjProd.Response == 0 || ObjDataAccess.Products.Find(id) == null)
        //            {
        //                ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record not found.", null);
        //            }
        //            else
        //            {
        //                ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", ObjDataAccess.Products.Find(id));
        //            }
        //        }
        //        else
        //        {
        //            var allErrors = ModelState.Values.SelectMany(x => x.Errors);
        //            ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error.", allErrors);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
        //    }

        //    return ObjResponse;
        //}

        public JsonResponse Delete(int id)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var DeletedSupplier = ObjDataAccess.Suppliers.Find(id);
                var ObjSupplier = ObjDataAccess.DeleteSupplierById(id);                
                if (ObjSupplier > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record deleted.", DeletedSupplier);
                }
                else if (ObjSupplier == 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Record not found", null);
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(x => x.Errors);
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }

            return ObjResponse;
        }



        // GET: api/Suppliers/5
        //[ResponseType(typeof(Supplier))]
        //public IHttpActionResult GetSupplier(int id)
        //{
        //    Supplier supplier = db.Suppliers.Find(id);
        //    if (supplier == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(supplier);
        //}

        // PUT: api/Suppliers/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutSupplier(int id, Supplier supplier)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != supplier.SupplierId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(supplier).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SupplierExists(id))
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

        //POST: api/Suppliers
       [ResponseType(typeof(Supplier))]
        public IHttpActionResult PostSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Suppliers.Add(supplier);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = supplier.SupplierId }, supplier);
        }

        // DELETE: api/Suppliers/5
        //[ResponseType(typeof(Supplier))]
        //public IHttpActionResult DeleteSupplier(int id)
        //{
        //    Supplier supplier = db.Suppliers.Find(id);
        //    if (supplier == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Suppliers.Remove(supplier);
        //    db.SaveChanges();

        //    return Ok(supplier);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool SupplierExists(int id)
        //{
        //    return db.Suppliers.Count(e => e.SupplierId == id) > 0;
        //}
    }
}