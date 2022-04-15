using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenApi.Models
{
    public class IsActiveUpdateModel
    {
        public int SupplierId { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}