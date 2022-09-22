using System.Collections.Generic;
using StartUp.Domain;
using StartUp.Domain.SearchCriterias;

namespace StartUp.IBusinessLogic;

public interface ISymptomManager
{
    List<Symptom> GetAllSale(SymptomSearchCriteria searchCriteria);
    Symptom GetSpecificSale(Symptom symptom);
    Symptom CreateSale(Symptom symptom);
    Request UpdateRequest(Symptom symptomUpdate);
    Symptom DeleteSale(Symptom symptom);
}