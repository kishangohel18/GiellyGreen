using System;
using DataAccessLayer.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface ISupplier
    {
        List<GetAllSupplier_Result> GetAllSupplier();
        dynamic AddSupplier(Supplier model);
        dynamic UpdateSupplier(int id, Supplier model);
        Supplier GetSupplierById(int id);
        void ToggleActiveStatus(int id, bool isactive);
        dynamic DeleteSupplierById(int id);
    }
}
