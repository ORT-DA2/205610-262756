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
    public class PharmacyService : IPharmacyService
    {

        private readonly IRepository<Pharmacy> _pharmacyRepository;

        public PharmacyService(IRepository<Pharmacy> pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        public List<Pharmacy> GetAllPharmacy(PharmacySearchCriteria searchCriteria)
        {
            var nameCriteria = searchCriteria.Name?.ToLower() ?? string.Empty;
            var addressCriteria = searchCriteria.Address?.ToLower() ?? string.Empty;
            var stockCriteria = searchCriteria.Stock ?? null;
            var requestCriteria = searchCriteria.Requests ?? null;

            Expression<Func<Pharmacy, bool>> pharmacyFilter = pharmacy =>
                pharmacy.Name.ToLower().Contains(nameCriteria) &&
                pharmacy.Address.ToLower().Contains(addressCriteria) &&
                pharmacy.Stock == stockCriteria && pharmacy.Requests == requestCriteria;

            return _pharmacyRepository.GetAllByExpression(pharmacyFilter).ToList();
        }

        public Pharmacy GetSpecificPharmacy(int pharmacyId)
        {
            var pharmacySaved = _pharmacyRepository.GetOneByExpression(p => p.Id == pharmacyId);

            if (pharmacySaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified pharmacy {pharmacyId}");
            }

            return pharmacySaved;
        }

        public Pharmacy CreatePharmacy(Pharmacy pharmacy)
        {
            pharmacy.isValidPharmacy();
            pharmacy.Requests = new List<Request>();
            NotExistInDataBase(pharmacy);

            _pharmacyRepository.InsertOne(pharmacy);
            _pharmacyRepository.Save();

            return pharmacy;
        }

        public Pharmacy UpdatePharmacy(int pharmacyId, Pharmacy updatedPharmacy)
        {
            updatedPharmacy.isValidPharmacy();

            var pharmacyStored = GetSpecificPharmacy(pharmacyId);

            pharmacyStored.Name = updatedPharmacy.Name;
            pharmacyStored.Address = updatedPharmacy.Address;

            _pharmacyRepository.UpdateOne(pharmacyStored);
            _pharmacyRepository.Save();

            return pharmacyStored;
        }

        public void DeletePharmacy(int pharmacyId)
        {
            var pharmacyStored = GetSpecificPharmacy(pharmacyId);

            _pharmacyRepository.DeleteOne(pharmacyStored);
            _pharmacyRepository.Save();
        }

        public void NotExistInDataBase(Pharmacy pharmacy)
        {
            var pharmacySaved = _pharmacyRepository.GetOneByExpression(p => p == pharmacy);

            if (pharmacySaved != null)
            {
                throw new InputException("Currently there is a pharmacy with that name");
            }
        }
    }
}
