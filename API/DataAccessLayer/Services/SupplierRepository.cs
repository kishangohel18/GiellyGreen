using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;

namespace DataAccessLayer.Services
{
    public class SupplierRepository : ISupplier
    {
        public GiellyGreen_SelfInvoiceEntities db = new GiellyGreen_SelfInvoiceEntities();

        public List<GetAllSupplier_Result> GetAllSupplier()
        {
            return db.GetAllSupplier(0).ToList();
        }

        public dynamic AddSupplier(Supplier model)
        {
            return db.InsertUpdateSupplier(0, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.LogoUrl, model.IsActive).FirstOrDefault();
        }

        public dynamic UpdateSupplier(int id, Supplier model)
        {
            return db.InsertUpdateSupplier(id, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.LogoUrl, model.IsActive).FirstOrDefault();
        }

        public Supplier GetSupplierById(int id)
        {
            return db.Suppliers.Find(id);
        }

        public void ToggleActiveStatus(int id, bool isactive)
        {
            db.UpdateStatus(id, isactive);
        }
        public dynamic DeleteSupplierById(int id)
        {
            return db.DeleteSupplier(id).FirstOrDefault();
        }

        public dynamic ActiveSupplier()
        {
            return db.GetActiveSupplier().ToList();
        }
        public dynamic ActiveSupplierInfo()
        {
            return db.GetActiveSupplierInfo().ToList();
        }
    }
}
