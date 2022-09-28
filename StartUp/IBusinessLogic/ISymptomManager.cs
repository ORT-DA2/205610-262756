using System.Collections.Generic;
using StartUp.Domain;
using StartUp.Domain.SearchCriterias;

namespace StartUp.IBusinessLogic;

public interface ISymptomManager
{
    List<Symptom> GetAllSymptom(SymptomSearchCriteria searchCriteria);
    Symptom GetSpecificSymptom(int symptomId);
    Symptom CreateSymptom(Symptom symptom);
    Symptom UpdateSymptom(int symptomId, Symptom symptomUpdate);
    void DeleteSymptom(int symptomId);
}