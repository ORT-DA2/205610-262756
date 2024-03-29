﻿using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
            var symptomCriteria = searchCriteria.SymptomDescription?.ToLower() ?? string.Empty;

            Expression<Func<Symptom, bool>> symptomFilter = symptom =>
                symptom.SymptomDescription.ToLower().Contains(symptomCriteria);

            return _symptomRepository.GetAllByExpression(symptomFilter).ToList();
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
            symptom.IsValidSymptom();

            _symptomRepository.InsertOne(symptom);
            _symptomRepository.Save();

            return symptom;
        }

        public Symptom UpdateSymptom(int symptomId, Symptom updatedSymptom)
        {
            updatedSymptom.IsValidSymptom();

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
