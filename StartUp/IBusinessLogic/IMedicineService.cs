﻿using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System.Collections.Generic;

namespace StartUp.IBusinessLogic
{
    public interface IMedicineService
    {
        List<Medicine> GetAllMedicine(MedicineSearchCriteria searchCriteria);
        Medicine GetSpecificMedicine(int medicineId);
        Medicine CreateMedicine(Medicine medicine);
        Medicine UpdateMedicine(int medicineId, Medicine medicineUpdate);
        void DeleteMedicine(int medicineId);
    }
}
