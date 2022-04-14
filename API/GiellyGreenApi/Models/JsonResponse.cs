using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenApi.Models
{
    public class JsonResponse
    {
        public int ResponseStatus { get; set; }
        public string Message { get; set; }
        public dynamic Result { get; set; }

    }
}