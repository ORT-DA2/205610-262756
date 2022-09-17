using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IPetitionManager
    {
        List<Petition> GetAllPetition(PetitionSearchCriteria searchCriteria);
        Petition GetSpecificPetition(string medicineCode);
        Petition CreatePetition(Petition petition);
        Petition UpdatePetition(Petition petitionUpdate);
        Petition DeletePetition(Petition petition);
    }
}
