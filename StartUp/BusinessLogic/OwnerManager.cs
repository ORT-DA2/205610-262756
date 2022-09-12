using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class OwnerManager : IOwnerManager
    {
        public Owner CreateOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public List<Owner> GetAllOwner(OwnerSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Owner GetSpecificOwner(int id)
        {
            throw new NotImplementedException();
        }
    }
}
