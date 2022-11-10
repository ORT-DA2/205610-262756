using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace StartUp.BusinessLogic
{
    public class PetitionService : IPetitionService
    {
        private readonly IRepository<Petition> _petitionRepository;
        private readonly IRepository<Pharmacy> _pharmacyRepository;
        private readonly IRepository<Medicine> _medicineRepository;
        private readonly ISessionService _sessionService;
       
        public PetitionService(IRepository<Petition> petitionRepository, IRepository<Pharmacy> pharmacyRepository,
                                                    ISessionService sessionService, IRepository<Medicine> medicineRepository)
        {
            _petitionRepository = petitionRepository;
            _pharmacyRepository = pharmacyRepository;
            _medicineRepository = medicineRepository;
            _sessionService = sessionService;
        }

        public List<Petition> GetAllPetition(PetitionSearchCriteria searchCriteria)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            var medicineCodeCriteria = searchCriteria.MedicineCode?.ToLower() ?? string.Empty;

            List<Petition> listSalved = new List<Petition>();

            foreach (Request request in pharmacy.Requests)
            {
                foreach (Petition petition in request.Petitions)
                {
                    if (petition.MedicineCode.Contains(medicineCodeCriteria))
                    {
                        listSalved.Add(petition);
                    }
                }
            }
            return listSalved;
        }

        public Petition GetSpecificPetition(int petitionId)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            var petitionSaved = _petitionRepository.GetOneByExpression(p => p.Id == petitionId);
            if(petitionSaved == null)
            {
                throw new InputException($"Could not find specified petition {petitionId}");
            }

            foreach (Request request in pharmacy.Requests)
            {
                if (request.Petitions.Contains(petitionSaved))
                {
                    return petitionSaved;
                }
            }

            throw new InvalidResourceException("The request does not belong to your pharmacy");
        }

        public Petition CreatePetition(Petition petition)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == _sessionService.UserLogged.Pharmacy.Name);

            petition.IsValidPetition();

            if (!ExistCodeMedicineInPharmacy(petition.MedicineCode, pharmacy))
            {
                throw new InvalidResourceException("The medicine code does not match any code within the pharmacy to which you belongs");
            }
            else
            {
                _petitionRepository.InsertOne(petition);
                _petitionRepository.Save();

                return petition;
            }
        }

        public Petition UpdatePetition(int petitionId, Petition updatedPetition)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            updatedPetition.IsValidPetition();

            var petitionStored = GetSpecificPetition(petitionId);

            bool update = false;

            foreach (Request request in pharmacy.Requests)
            {
                foreach (Petition pet in request.Petitions)
                {
                    if (pet.Id == petitionStored.Id)
                    {
                        if (ExistCodeMedicineInPharmacy(updatedPetition.MedicineCode, pharmacy))
                        {
                            petitionStored.MedicineCode = updatedPetition.MedicineCode;
                            petitionStored.Amount = updatedPetition.Amount;

                            _petitionRepository.UpdateOne(petitionStored);
                            _petitionRepository.Save();
                            update = true;

                            return petitionStored;
                        }
                        else
                        {
                            throw new InvalidResourceException("The medicine code you want to insert in the request does not exist among the medicines in the pharmacy");
                        }
                    }
                }
            }
            throw new InvalidResourceException("The petition does not belong to any pharmacy request");
        }

        public void DeletePetition(int petitionId)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);
            bool delete = false;
            var petitionStored = GetSpecificPetition(petitionId);

            foreach (Request request in pharmacy.Requests)
            {
                foreach (Petition pet in request.Petitions)
                {
                    if (pet.Id == petitionStored.Id)
                    {
                        request.Petitions.Remove(pet);
                        _pharmacyRepository.UpdateOne(pharmacy);
                        _pharmacyRepository.Save();
                        _petitionRepository.DeleteOne(petitionStored);
                        _petitionRepository.Save();
                        delete = true;
                    }
                }
            }
            if (!delete)
            {
                throw new InvalidResourceException("The request you want to delete does not belong to any request from your pharmacy");
            }
        }

        private bool ExistCodeMedicineInPharmacy(string medicineCode, Pharmacy pharmacy)
        {
            var medicine = _medicineRepository.GetAllByExpression(m => m.Code == medicineCode).ToList();

            foreach (Medicine med in medicine)
            {
                if (pharmacy.Stock.Contains(med))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
