using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IPharmacyManager
    {
        List<Pharmacy> GetAllPharmacy(PharmacySearchCriteria searchCriteria);
        Pharmacy GetSpecificPharmacy(int pharmacyId);
        Pharmacy CreatePharmacy(Pharmacy pharmacy);
        Pharmacy UpdatePharmacy(int pharmacyId, Pharmacy pharmacyUpdate);
        void DeletePharmacy(int pharmacyId);
    }
}
