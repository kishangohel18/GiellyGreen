using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GiellyGreenApi.Models
{
    public class CompanyProfileViewModel
    {
        public int ProfileId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public Nullable<decimal> DefaultVat { get; set; }

    }
}