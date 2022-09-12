using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IMedicineManager
    {
        List<Medicine> GetAllMedicine(MedicineSearchCriteria searchCriteria);
        Medicine GetSpecificMedicine();
        Medicine CreateMedicine();
    }
}
