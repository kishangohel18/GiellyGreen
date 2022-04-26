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
            var ObjSupplierList = ObjDataAccess.GetAllSupplier(0).ToList();
            return ObjSupplierList;
        }

        public dynamic AddSupplier(Supplier model)
        {
            var Obj = ObjDataAccess.InsertUpdateSupplier(0, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.LogoUrl, model.IsActive).FirstOrDefault();
            return Obj;
        }

        public dynamic UpdateSupplier(int id, Supplier model)
        {
            var Obj = ObjDataAccess.InsertUpdateSupplier(id, model.SupplierName, model.SupplierReference, model.BusinessAddress, model.Email, model.Phone, model.TaxReference, model.CompanyRegNumber, model.CompanyRegAddress, model.VatNumber, model.LogoUrl, model.IsActive).FirstOrDefault();
            return Obj;
        }

        public Supplier GetSupplierById(int id)
        {
            Supplier Supplier = ObjDataAccess.Suppliers.Find(id);
            return Supplier;
        }
        public void ToggleActiveStatus(int id, bool isactive)
        {
            ObjDataAccess.UpdateStatus(id, isactive);
        }
        public dynamic DeleteSupplierById(int id)
        {
            var ObjSupplier = ObjDataAccess.DeleteSupplier(id).FirstOrDefault();
            return ObjSupplier;
        }

    }
}
