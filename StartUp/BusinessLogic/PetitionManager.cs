using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class PetitionManager : IPetitionManager
    {
        public Petition CreatePetition()
        {
            throw new NotImplementedException();
        }

        public Petition CreatePetition(Petition petition)
        {
            throw new NotImplementedException();
        }

        public Petition DeletePetition(Petition petition)
        {
            throw new NotImplementedException();
        }

        public List<Petition> GetAllPetition(PetitionSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Petition GetSpecificPetition()
        {
            throw new NotImplementedException();
        }

        public Petition GetSpecificPetition(string medicineCode)
        {
            throw new NotImplementedException();
        }

        public Petition UpdatePetition(Petition petitionUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
