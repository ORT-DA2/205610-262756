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
        Pharmacy GetSpecificPharmacy(string name);
        Pharmacy CreatePharmacy(Pharmacy pharmacy);
        Pharmacy UpdatePharmacy(string name, Pharmacy pharmacyUpdate);
        Pharmacy DeletePharmacy(string name);
    }
}
