using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using DataAccessLayer.Services;
using GiellyGreenApi.ActionFilter;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;
using AutoMapper;



namespace GiellyGreenApi.Controllers
{

    [Authorize]
    public class SuppliersController : ApiController
    {
        public static JsonResponse ObjResponse = new JsonResponse();
        private readonly ISupplier SupplierRepository = new SupplierRepository();

        public JsonResponse Get()
        {
            try
            {
                var ObjSupplierList = SupplierRepository.GetAllSupplier();

                ObjSupplierList.ForEach(supplier =>
                {
                    string path = HttpContext.Current.Server.MapPath("~/ImageStorage");

                    if (!string.IsNullOrEmpty(supplier.LogoUrl) && supplier.LogoUrl != "null")
                    {
                        string imgPath = Path.Combine(path, supplier.LogoUrl);
                        byte[] imageByte = File.ReadAllBytes(imgPath);
                        supplier.LogoUrl = Convert.ToBase64String(imageByte);
                    }
                });

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

        public JsonResponse Post(SupplierViewModel model)
        {
            SupplierHelper.TrimWhiteSpaceOnRequest(model);
            try
            {
                if (ModelState.IsValid)
                {
                    model.LogoUrl = SupplierHelper.setLogo(model.SupplierName, model.SupplierReference, model.LogoUrl);

                    ObjResponse = SupplierHelper.CheckDuplicate(model.SupplierId, model);
                    if (ObjResponse.ResponseStatus != 0)
                    {
                        var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<SupplierViewModel, Supplier>());
                        var mapper = config.CreateMapper();
                        var ObjSupplierMapper = mapper.Map<Supplier>(model);
                        var ObjSupplier = SupplierRepository.AddSupplier(ObjSupplierMapper);

                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record created.", model);
                    }
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }
            return ObjResponse;
        }

        [CustomFilter]
        public JsonResponse Put(int id, SupplierViewModel model)
        {
            SupplierHelper.TrimWhiteSpaceOnRequest(model);
            try
            {
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg =>
                          cfg.CreateMap<SupplierViewModel, Supplier>());
                    var mapper = config.CreateMapper();
                    var ObjSupplierMapper = mapper.Map<Supplier>(model);

                    model.LogoUrl = SupplierHelper.setLogo(model.SupplierName?.Trim(), model.SupplierReference?.Trim(), model.LogoUrl.Trim());
                    ObjResponse = SupplierHelper.CheckDuplicate(id, model);

                    if (ObjResponse.ResponseStatus != 0)
                    {
                        var ObjSupplier = SupplierRepository.UpdateSupplier(id, ObjSupplierMapper);

                        if (SupplierRepository.GetSupplierById(id) == null)
                        {
                            ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Record not found.", null);
                        }
                        else
                        {
                            ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", model);
                        }
                    }
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2  , "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }
            return ObjResponse;
        }

        [Route("ToggleActive")]
        public JsonResponse ToggleActive(int id, bool IsActive)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SupplierRepository.ToggleActiveStatus(id, IsActive);
                    if (SupplierRepository.GetSupplierById(id) == null)
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Record not found.", null);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record updated.", SupplierRepository.GetSupplierById(id));
                    }
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(2, ex.Message, null);
            }
            return ObjResponse;
        }

        public JsonResponse Delete(int id)
        {
            try
            {
                var CurrentSupplier = SupplierRepository.GetSupplierById(id);
                var ObjSupplier = SupplierRepository.DeleteSupplierById(id);

                if (CurrentSupplier == null)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Record not found.", null);
                }
                else if (ObjSupplier.ResponseStatus == 1)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record deleted", CurrentSupplier);
                }
                else if (ObjSupplier.ResponseStatus == 2)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Record cannot delete. Because invoice present for this supplier.", CurrentSupplier);
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Error", allErrors);
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
