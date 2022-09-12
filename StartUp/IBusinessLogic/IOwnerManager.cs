using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IOwnerManager
    {
        List<Owner> GetAllOwner(OwnerSearchCriteria searchCriteria);
        Owner GetSpecificOwner(int id);
        Owner CreateOwner(Owner owner);
    }
}
