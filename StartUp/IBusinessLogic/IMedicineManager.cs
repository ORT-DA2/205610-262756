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
        Medicine GetSpecificMedicine(int medicineId);
        Medicine CreateMedicine(Medicine medicine);
        Medicine UpdateMedicine(int medicineId, Medicine medicineUpdate);
        void DeleteMedicine(int medicineId);
    }
}
