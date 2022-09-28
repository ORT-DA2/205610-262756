using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.BusinessLogic
{
    public class SymptomManager : ISymptomManager
    {
        private readonly IRepository<Symptom> _symptomRepository;

        public SymptomManager(IRepository<Symptom> symptomRepository)
        {
            _symptomRepository = symptomRepository;
        }

        public List<Symptom> GetAllSymptom(SymptomSearchCriteria searchCriteria)
        {
            var symptomCriteria = searchCriteria.SymptomDescription?.ToLower() ?? string.Empty;

            Expression<Func<Symptom, bool>> symptomFilter = symptom =>
                symptom.SymptomDescription.ToLower().Contains(symptomCriteria);

            return _symptomRepository.GetAllExpression(symptomFilter).ToList();
        }

        public Symptom GetSpecificSymptom(int symptomId)
        {
            Symptom symptomSaved = _symptomRepository.GetOneByExpression(s => s.Id == symptomId);

            if (symptomSaved == null)
            {
                throw new ResourceNotFoundException($"The symptom {symptomId} not exist");
            }
            return symptomSaved;
        }

        public Symptom CreateSymptom(Symptom symptom)
        {
            symptom.isValidSymptom();

            _symptomRepository.InsertOne(symptom);
            _symptomRepository.Save();

            return symptom;
        }

        public Symptom UpdateSymptom(int symptomId, Symptom updatedSymptom)
        {
            updatedSymptom.isValidSymptom();

            var symptomStored = GetSpecificSymptom(symptomId);

            symptomStored.SymptomDescription = updatedSymptom.SymptomDescription;

            _symptomRepository.UpdateOne(symptomStored);
            _symptomRepository.Save();

            return symptomStored;
        }

        public void DeleteSymptom(int symptomId)
        {
            var symptomStored = GetSpecificSymptom(symptomId);

            _symptomRepository.DeleteOne(symptomStored);
            _symptomRepository.Save();
        }

    }
}
