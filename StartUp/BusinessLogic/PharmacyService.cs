using StartUp.Domain;
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
    public class PharmacyService : IPharmacyService
    {

        private readonly IRepository<Pharmacy> _pharmacyRepository;

        public PharmacyService(IRepository<Pharmacy> pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        public List<Pharmacy> GetAllPharmacy(PharmacySearchCriteria searchCriteria)
        {
            Expression<Func<Pharmacy, bool>> pharmacyFilter = pharmacy => true;

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
            NotExistInDataBase(pharmacy);
            pharmacy.Stock = new List<Medicine>();
            pharmacy.Sales = new List<Sale>();
            pharmacy.Requests = new List<Request>();

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

        private void NotExistInDataBase(Pharmacy pharmacy)
        {
            var pharmacySaved = _pharmacyRepository.GetOneByExpression(p => p.Name == pharmacy.Name);

            if (pharmacySaved != null)
            {
                throw new InputException("Currently there is a pharmacy with that name");
            }
        }
    }
}
