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

        
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //var a = actionContext.ActionArguments.Values;
            var dictionary = actionContext.ActionArguments;
            //key will contain the key, for convenience.
            foreach (var key in dictionary.Keys)
            {
                //the value
                var val = dictionary[key];
                
                //if (val is string)
                //{

                    Type objType = val.GetType();
                    PropertyInfo[] properties = objType.GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        object propValue = property.GetValue(val, null);
                        //var elems = propValue as IList;
                        //if (elems != null)
                        //{
                        //    foreach (var item in elems)
                        //    {
                        //        PrintProperties(item, indent + 3);
                        //    }
                        //}
                        //else
                        //{
                        //    // This will not cut-off System.Collections because of the first check
                        //    if (property.PropertyType.Assembly == objType.Assembly)
                        //    {
                        //        Console.WriteLine("{0}{1}:", indentString, property.Name);

                        //        PrintProperties(propValue, indent + 2);
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine("{0}{1}: {2}", indentString, property.Name, propValue);
                        //    }
                        //}
                    //}
                    //the value is runtime type of string
                    //do what you want with it.
                }
            }
            
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