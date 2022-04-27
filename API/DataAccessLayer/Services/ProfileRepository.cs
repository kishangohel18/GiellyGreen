using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System.Linq;

namespace DataAccessLayer.Services
{
    public class ProfileRepository : IProfile
    {
        public GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();

        public dynamic GetCompanyProfile()
        {
            return db.GetCompanyProfile().ToList();
        }

        public dynamic InsertUpdateCompanyProfile(CompanyProfile model)
        {
            dynamic[] ObjResponse = new dynamic[3];
            if (model.ProfileId == 0)
            {
                db.InsertUpdateCompanyProfile(model.ProfileId, model.CompanyName, model.AddressLine, model.City, model.Zipcode, model.Country, model.DefaultVat).FirstOrDefault();
                ObjResponse[0] = 1;
                ObjResponse[1] = "Record created.";
                ObjResponse[2] = model;
                return ObjResponse;
            }
            else
            {
                db.InsertUpdateCompanyProfile(model.ProfileId, model.CompanyName, model.AddressLine, model.City, model.Zipcode, model.Country, model.DefaultVat).FirstOrDefault();
                ObjResponse[0] = 1;
                ObjResponse[1] = "Record updated.";
                ObjResponse[2] = model;
                return ObjResponse;
            }
        }

    }
}
