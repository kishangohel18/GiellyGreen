using GiellyGreenApi.Helper;
using GiellyGreenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GiellyGreenApi.ActionFilter
{
    public class CustomFilter : ActionFilterAttribute
    {
        //public CustomFilter(SupplierViewModel model)
        //{
        //    PropertyInfo[] properties = model.GetType().GetProperties();
        //    foreach (PropertyInfo property in properties)
        //    {
                
        //            if (property.PropertyType == typeof(string))
        //            {
        //                var o = property.GetValue(model, null) ?? "";
        //                string s = (string)o;
        //                property.SetValue(model, s.Trim());
        //            }
                   
                
               
        //    }
            
        //}
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            SupplierViewModel TrimWhiteSpaceOnRequest<SupplierViewModel>(SupplierViewModel model)
            {
                var ObjResponse = new JsonResponse();

                if (model != null)
                {
                    PropertyInfo[] properties = model.GetType().GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        try
                        {
                            if (property.PropertyType == typeof(string))
                            {
                                var o = property.GetValue(model, null) ?? "";
                                string s = (string)o;
                                property.SetValue(model, s.Trim());
                            }
                            else
                            {
                                //handle nested Friend object here

                            }
                        }
                        catch (Exception)
                        {
                            //ObjResponse = JsonResponseHelper.JsonResponseMessage(1, "Error", null);

                            //log.info("Error converting field " + field.getName());
                        }
                    }

                }
                return model;
            }

        }


    }
}