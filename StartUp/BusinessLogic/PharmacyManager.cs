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
    public class PharmacyManager : IPharmacyManager
    {

        private readonly IRepository<Pharmacy> _pharmacyRepository;

        public PharmacyManager(IRepository<Pharmacy> pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        public List<Pharmacy> GetAllPharmacy(PharmacySearchCriteria searchCriteria)
        {
            var nameCriteria = searchCriteria.Name?.ToLower() ?? string.Empty;
            var addressCriteria = searchCriteria.Address?.ToLower() ?? string.Empty;
            //   HAY QUE AGREGAR LAS LISTAS DE MEDICAMENTOS Y SOLICITUDES?
            // var stockCriteria = searchCriteria.Stock?.ToString() ?? string.Empty;
            //var requestCriteria = searchCriteria.Requests?.ToString() ?? string.Empty;

            Expression<Func<Pharmacy, bool>> pharmacyFilter = pharmacy =>
                pharmacy.Name.ToLower().Contains(nameCriteria) &&
                pharmacy.Address.ToLower().Contains(addressCriteria);

            return _pharmacyRepository.GetAllExpression(pharmacyFilter).ToList();
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
    }
}
