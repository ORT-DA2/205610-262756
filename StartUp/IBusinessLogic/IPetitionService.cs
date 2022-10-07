using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IPetitionService
    {
        List<Petition> GetAllPetition(PetitionSearchCriteria searchCriteria);
        Petition GetSpecificPetition(int petitionId);
        Petition CreatePetition(Petition petition);
        Petition UpdatePetition(int petitionId, Petition petitionUpdate);
        void DeletePetition(int petitionId);
    }
}
