using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class MedicineManager : IMedicineManager
    {
        public Medicine CreateMedicine()
        {
            throw new NotImplementedException();
        }

        public Medicine CreateMedicine(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public Medicine DeleteMedicine(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public List<Medicine> GetAllMedicine(MedicineSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Medicine GetSpecificMedicine()
        {
            throw new NotImplementedException();
        }

        public Medicine GetSpecificMedicine(string code)
        {
            throw new NotImplementedException();
        }

        public Medicine UpdateMedicine(Medicine medicineUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
