using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class PharmacyManager : IPharmacyManager
    {
        public Pharmacy CreatePharmacy()
        {
            throw new NotImplementedException();
        }

        public List<Pharmacy> GetAllPharmacy(PharmacySearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Pharmacy GetSpecificPharmacy()
        {
            throw new NotImplementedException();
        }
    }
}
