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
    public class SymptomService : ISymptomService
    {
        private readonly IRepository<Symptom> _symptomRepository;

        public SymptomService(IRepository<Symptom> symptomRepository)
        {
            _symptomRepository = symptomRepository;
        }

        public List<Symptom> GetAllSymptom(SymptomSearchCriteria searchCriteria)
        {
            var symptomCriteria = searchCriteria.Symptom?.ToLower() ?? string.Empty;

            Expression<Func<Symptom, bool>> symptomFilter = symptom =>
                symptom.SymptomDescription.ToLower().Contains(symptomCriteria);

            return _symptomRepository.GetAllByExpression(symptomFilter).ToList();
        }

        public Symptom GetSpecificSymptom(int id)
        {
            Symptom symptomSaved = _symptomRepository.GetOneByExpression(s => s.Id == id);

            if (symptomSaved == null)
            {
                throw new ResourceNotFoundException($"The symptom {id} not exist");
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

        public Symptom UpdateSymptom(int id, Symptom updatedSymptom)
        {
            updatedSymptom.isValidSymptom();

            var symptomStored = GetSpecificSymptom(id);

            symptomStored.SymptomDescription = updatedSymptom.SymptomDescription;

            _symptomRepository.UpdateOne(symptomStored);
            _symptomRepository.Save();

            return symptomStored;
        }

        public void DeleteSymptom(int id)
        {
            var symptomStored = GetSpecificSymptom(id);

            _symptomRepository.DeleteOne(symptomStored);
            _symptomRepository.Save();
        }

    }
}
