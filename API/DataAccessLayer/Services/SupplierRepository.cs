using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interface;
using DataAccessLayer.Model;

namespace DataAccessLayer.Services
{
    public class SupplierRepository : ISupplier
    {
        public GiellyGreen_SelfInvoiceEntities ObjDataAccess = new GiellyGreen_SelfInvoiceEntities();

        public List<GetAllSupplier_Result> GetAllSupplier()
        {
            return ObjDataAccess.GetAllSupplier(0).ToList();
        }

        public dynamic AddSupplier(Supplier model)
        {
            return ObjDataAccess.InsertUpdateSupplier(0, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.LogoUrl, model.IsActive).FirstOrDefault();
        }

        public dynamic UpdateSupplier(int id, Supplier model)
        {
            return ObjDataAccess.InsertUpdateSupplier(id, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.LogoUrl, model.IsActive).FirstOrDefault();
        }

        public Supplier GetSupplierById(int id)
        {
            return ObjDataAccess.Suppliers.Find(id);
        }
        public void ToggleActiveStatus(int id, bool isactive)
        {
            ObjDataAccess.UpdateStatus(id, isactive);
        }
        public dynamic DeleteSupplierById(int id)
        {
            return ObjDataAccess.DeleteSupplier(id).FirstOrDefault();
        }

        public dynamic ActiveSupplier()
        {
            return ObjDataAccess.GetActiveSupplier().ToList();
        }

    }
}
