using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Configuration;
using System.Web.Http;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Http.Cors;


namespace GiellyGreenApi.Controllers
{
    [EnableCors(origins: "http://3fb2-106-201-236-89.ngrok.io/", headers: "*", methods: "*")]

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
                        //var accessToken = string.Empty;
                        //var keyValues = new List<KeyValuePair<string, string>>
                        //{
                        //    new KeyValuePair<string, string>("email",model.email),
                        //    new KeyValuePair<string, string>("password",model.password),
                        //    new KeyValuePair<string, string>("grant_type","password")
                        //};

                        //var request = new HttpRequestMessage(HttpMethod.Post, Url + "Token");
                        //request.Content = new FormUrlEncodedContent(keyValues);


                        //var client = new HttpClient();
                        //var response = client.SendAsync(request).Result;

                        //using (HttpContent content = response.Content)
                        //{
                        //    var json = content.ReadAsStringAsync();
                        //    JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(json.Result);
                        //    var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
                        //    accessToken = jwtDynamic.Value<string>("access_token");
                        //    var Username = jwtDynamic.Value<string>("username");
                        //    var AccessTokenExpirationDate = accessTokenExpiration;

                        //}

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
