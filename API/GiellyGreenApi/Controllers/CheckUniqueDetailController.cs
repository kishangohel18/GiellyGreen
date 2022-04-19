using DataAccessLayer.Model;
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
    public class CheckUniqueDetailController : ApiController
    {

        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        [Route("VarifyEmail")]
        public JsonResponse CheckEmail(int id, string email)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (id == 0)
                {

                    if (ObjDataAccess.Suppliers.Any(s => s.Email == email) && email != null && email != "")
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
                    if (ObjDataAccess.Suppliers.Any(s => s.Email == email && s.SupplierId != id) && email != null && email != "")
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
        public JsonResponse CheckReferenceNumber(int id, string ReferenceNumber)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if(id == 0)
                {
                    if (ObjDataAccess.Suppliers.Any(s => s.ReferenceNumber == ReferenceNumber) && ReferenceNumber != null && ReferenceNumber != "")
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", ReferenceNumber);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", ReferenceNumber);
                    }
                }
                else
                {
                    if (ObjDataAccess.Suppliers.Any(s => s.ReferenceNumber == ReferenceNumber && s.SupplierId != id) && ReferenceNumber != null && ReferenceNumber != "")
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Email should be unique", ReferenceNumber);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Correct email", ReferenceNumber);
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
                if(id == 0)
                {
                    if (ObjDataAccess.Suppliers.Any(s => s.VatNumber == VatNumber) && VatNumber != null && VatNumber != "")
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
                    if (ObjDataAccess.Suppliers.Any(s => s.VatNumber == VatNumber && s.SupplierId != id) && VatNumber != null && VatNumber != "")
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
                if(id == 0)
                {
                    if (ObjDataAccess.Suppliers.Any(s => s.TaxReference == TaxReference) && TaxReference != null && TaxReference != "")
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
                    if (ObjDataAccess.Suppliers.Any(s => s.TaxReference == TaxReference && s.SupplierId != id) && TaxReference != null && TaxReference != "")
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
