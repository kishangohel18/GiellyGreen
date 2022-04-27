using AutoMapper;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using DataAccessLayer.Services;
using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace GiellyGreenApi.Controllers
{

    [Authorize]
    public class ProfileController : ApiController
    {

        public static JsonResponse ObjResponse = new JsonResponse();
        private readonly IProfile ProfileRepository = new ProfileRepository();

        public JsonResponse Get()
        {
            try
            {
                var ObjCompanyProfile = ProfileRepository.GetCompanyProfile();
                if (ObjCompanyProfile != null)
                {
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Record found.", ObjCompanyProfile);
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

        public JsonResponse InsertUpdateCompanyProfile(CompanyProfileViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var config = new MapperConfiguration(cfg =>
                          cfg.CreateMap<CompanyProfileViewModel, CompanyProfile>());
                    var mapper = config.CreateMapper();
                    var ObjProfileMapper = mapper.Map<CompanyProfile>(model);

                    var Response = ProfileRepository.InsertUpdateCompanyProfile(ObjProfileMapper);

                    ObjResponse.ResponseStatus = Response[0];
                    ObjResponse.Message = Response[1];
                    ObjResponse.Result = Response[2];
                }
                else
                {
                    var allErrors = ModelState.Values.SelectMany(E => E.Errors).Select(E => E.ErrorMessage).ToList();
                    ObjResponse = JsonResponseHelper.JsonResponseMessage(2, "Error.", allErrors);
                }
            }
            catch (Exception ex)
            {
                ObjResponse = JsonResponseHelper.JsonResponseMessage(0, ex.Message, null);
            }
            return ObjResponse;
        }

    }
}
