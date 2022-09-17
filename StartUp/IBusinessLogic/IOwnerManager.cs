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
        Owner GetSpecificOwner(string email);
        Owner CreateOwner(Owner owner);
        Owner UpdateOwner(Owner ownerUpdate);
        Owner DeleteOwner(string email);

    }
}
