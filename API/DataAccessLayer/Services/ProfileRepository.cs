using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class ProfileRepository : IProfile
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        public dynamic GetCompanyProfile()
        {
            return ObjDataAccess.GetCompanyProfile().ToList();
        }

        public dynamic InsertUpdateCompanyProfile(CompanyProfile model)
        {
            dynamic[] ObjResponse = new dynamic[3];
            if (model.ProfileId == 0)
            {
                ObjDataAccess.InsertUpdateCompanyProfile(model.ProfileId, model.CompanyName, model.AddressLine, model.City, model.Zipcode, model.Country, model.DefaultVat).FirstOrDefault();
                ObjResponse[0] = 1;
                ObjResponse[1] = "Record created.";
                ObjResponse[2] = model;
                return ObjResponse;
            }
            else
            {
                ObjDataAccess.InsertUpdateCompanyProfile(model.ProfileId, model.CompanyName, model.AddressLine, model.City, model.Zipcode, model.Country, model.DefaultVat).FirstOrDefault();
                ObjResponse[0] = 1;
                ObjResponse[1] = "Record updated.";
                ObjResponse[2] = model;
                return ObjResponse;
            }
        }

    }
}
