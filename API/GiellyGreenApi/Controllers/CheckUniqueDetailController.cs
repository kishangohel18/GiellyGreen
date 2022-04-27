using DataAccessLayer.Model;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace GiellyGreenApi.Controllers
{
    public class CheckUniqueDetailController : ApiController
    {

        public GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();

        [Route("VarifyEmail")]
        public JsonResponse CheckEmail(int id, string email)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (id == 0)
                {

                    if (db.Suppliers.Any(s => s.Email == email) && email != null)
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", email);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", email);
                    }
                }
                else
                {
                    if (db.Suppliers.Any(s => s.Email == email && s.SupplierId != id) && email != null)
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", email);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", email);
                    }
                }

            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }

            return ObjResponse;
        }


        [Route("VarifyRefenceNumber")]
        public JsonResponse CheckReferenceNumber(int id, string SupplierReference)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (id == 0)
                {
                    if (db.Suppliers.Any(s => s.SupplierReference == SupplierReference) && SupplierReference != null && SupplierReference != "")
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", SupplierReference);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", SupplierReference);
                    }
                }
                else
                {
                    if (db.Suppliers.Any(s => s.SupplierReference == SupplierReference && s.SupplierId != id) && SupplierReference != null && SupplierReference != "")
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", SupplierReference);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", SupplierReference);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }

            return ObjResponse;
        }

        [Route("VarifyVatNumber")]
        public JsonResponse CheckVatNumber(int id, string VatNumber)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (id == 0)
                {
                    if (db.Suppliers.Any(s => s.VatNumber == VatNumber) && VatNumber != null && VatNumber != "")
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", VatNumber);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", VatNumber);
                    }
                }
                else
                {
                    if (db.Suppliers.Any(s => s.VatNumber == VatNumber && s.SupplierId != id) && VatNumber != null && VatNumber != "")
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", VatNumber);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", VatNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }

            return ObjResponse;
        }


        [Route("VarifyTaxReference")]
        public JsonResponse CheckTaxReference(int id, string TaxReference)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (id == 0)
                {
                    if (db.Suppliers.Any(s => s.TaxReference == TaxReference) && TaxReference != null && TaxReference != "")
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", TaxReference);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", TaxReference);
                    }
                }
                else
                {
                    if (db.Suppliers.Any(s => s.TaxReference == TaxReference && s.SupplierId != id) && TaxReference != null && TaxReference != "")
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", TaxReference);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", TaxReference);
                    }
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
