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

        public List<Medicine> GetAllMedicine(MedicineSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Medicine GetSpecificMedicine()
        {
            throw new NotImplementedException();
        }
    }
}
