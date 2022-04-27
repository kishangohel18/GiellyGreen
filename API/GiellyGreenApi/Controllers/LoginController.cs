using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;
using System;
using System.Linq;
using System.Configuration;
using System.Web.Http;


namespace GiellyGreenApi.Controllers
{
    public class LoginController : ApiController
    {
        public JsonResponse LoginPost(LoginModel model)
        {
            var ObjResponse = new JsonResponse();
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.email == ConfigurationManager.AppSettings["email"].ToString() && model.password == ConfigurationManager.AppSettings["password"].ToString())
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Success.", model);
                    }
                    else
                    {
                        ObjResponse = JsonResponseHelper.JsonResponseMessage(0, "Fail.", null);
                    }
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
    }
}
