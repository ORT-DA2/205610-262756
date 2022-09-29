using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IOwnerService
    {
        List<Owner> GetAllOwner(OwnerSearchCriteria searchCriteria);
        Owner GetSpecificOwner(int ownerId);
        Owner CreateOwner(Owner owner);
        Owner UpdateOwner(int ownerId, Owner ownerUpdate);
        void DeleteOwner(int ownerId);

    }
}
