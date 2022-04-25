using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiellyGreenApi.Models
{
    public class UpdateActiveStatus
    {
        public int SupplierId { get; set; }
        public bool IsActive { get; set; }
    }
}