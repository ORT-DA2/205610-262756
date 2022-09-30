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

namespace StartUp.BusinessLogic
{
    public class PetitionService : IPetitionService
    {
        private readonly IRepository<Petition> _petitionRepository;

        public PetitionService(IRepository<Petition> petitionRepository)
        {
            _petitionRepository = petitionRepository;
        }

        public List<Petition> GetAllPetition(PetitionSearchCriteria searchCriteria)
        {
            var medicineCodeCriteria = searchCriteria.MedicineCode?.ToLower() ?? string.Empty;
            var amountCriteria = searchCriteria.Amount?.ToString().ToLower() ?? string.Empty;

            Expression<Func<Petition, bool>> petitionFilter = petition =>
                petition.Amount.ToString().ToLower().Contains(amountCriteria) &&
                petition.MedicineCode.ToLower().Contains(medicineCodeCriteria);

            return _petitionRepository.GetAllByExpression(petitionFilter).ToList();
        }

        public Petition GetSpecificPetition(int petitionId)
        {
            var petitionSaved = _petitionRepository.GetOneByExpression(p => p.Id == petitionId);

            if (petitionSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified petition {petitionId}");
            }
            return petitionSaved;
        }

        public Petition CreatePetition(Petition petition)
        {
            petition.isValidPetition();

            _petitionRepository.InsertOne(petition);
            _petitionRepository.Save();

            return petition;
        }

        public Petition UpdatePetition(int petitionId, Petition updatedPetition)
        {
            updatedPetition.isValidPetition();

            var petitionStored = GetSpecificPetition(petitionId);

            petitionStored.MedicineCode = updatedPetition.MedicineCode;
            petitionStored.Amount = updatedPetition.Amount;

            _petitionRepository.UpdateOne(petitionStored);
            _petitionRepository.Save();

            return petitionStored;
        }

        public void DeletePetition(int petitionId)
        {
            var petitionStored = GetSpecificPetition(petitionId);

            _petitionRepository.DeleteOne(petitionStored);
            _petitionRepository.Save();
        }

    }
}
