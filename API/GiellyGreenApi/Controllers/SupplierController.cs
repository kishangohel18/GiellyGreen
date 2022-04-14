using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GiellyGreenApi.Controllers
{
    public class SupplierController : ApiController
    {

        //public Product_DemoEntities ObjDataAccess = new Product_DemoEntities();

        // GET api/values/5
        public JsonResponse GetAllSupplier(string Name = "")
        {
            var ObjResponse = new JsonResponse();
            try
            {
                //List<Product> ObjProductLists = null;
                //List<Product> ObjProductLists = Repository.GetProducts(Name);

                if (string.IsNullOrEmpty(Name))
                {
                    ObjProductLists = ObjDataAccess.Products.ToList();
                }
                else
                {
                    ObjProductLists = ObjDataAccess.Products.Where(m => m.Name.Contains(Name)).ToList();
                }

                if (ObjProductLists != null && ObjProductLists.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Total " + ObjProductLists.Count + " records found.", ObjProductLists);
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

        public JsonResponse GetActiveSupplier(string Name = "")
        {
            var ObjResponse = new JsonResponse();
            try
            {
                //List<Product> ObjProductLists = null;
                //List<Product> ObjProductLists = Repository.GetProducts(Name);

                if (string.IsNullOrEmpty(Name))
                {
                    ObjProductLists = ObjDataAccess.Products.ToList();
                }
                else
                {
                    ObjProductLists = ObjDataAccess.Products.Where(m => m.Name.Contains(Name)).ToList();
                }

                if (ObjProductLists != null && ObjProductLists.Count > 0)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Total " + ObjProductLists.Count + " records found.", ObjProductLists);
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

        // POST api/values
        public JsonResponse Post(ProductViewModel model)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {


                    string GetUserId = User.Identity.GetUserId();
                    var ObjProd = ProductHelper.AddProduct(model, GetUserId, null, ObjDataAccess);

                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record created.", ObjProd);
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(x => x.Errors);
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }

            return ObjResponse;
        }

        // PUT api/values/5
        public JsonResponse Put(int id, ProductViewModel model)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var ObjProd = ObjDataAccess.Products.Find(id);
                if (ModelState.IsValid && ObjProd != null)
                {
                    string GetUserId = User.Identity.GetUserId();
                    ObjProd = ProductHelper.AddProduct(model, GetUserId, ObjProd, ObjDataAccess);

                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", ObjProd);
                }
                else if (ObjProd == null)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Record not found.", null);
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(x => x.Errors);
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }

            return ObjResponse;
        }

        // DELETE api/values/5
        public JsonResponse Delete(int id)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                var ObjProd = ObjDataAccess.Products.Find(id);
                if (ObjProd != null)
                {
                    ObjDataAccess.Products.Remove(ObjProd);
                    ObjDataAccess.SaveChanges();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record deleted.", ObjProd);
                }
                else if (ObjProd == null)
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

    }
}
