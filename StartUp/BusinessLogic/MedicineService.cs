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
    public class MedicineService : IMedicineService
    {
        private readonly IRepository<Medicine> _medicineRepository;
        private readonly ISessionService _sessionService;
        private readonly IRepository<Pharmacy> _pharmacyRepository;
        public MedicineService(IRepository<Medicine> medicineRepository, ISessionService sessionService, IRepository<Pharmacy> pharmacyRepository)
        {
            _medicineRepository = medicineRepository;
            _sessionService = sessionService;
            _pharmacyRepository = pharmacyRepository;
        }

        public List<Medicine> GetAllMedicine(MedicineSearchCriteria searchCriteria)
        {
            var nameCriteria = searchCriteria.Name?.ToLower() ?? string.Empty;

            Expression<Func<Medicine, bool>> medicineFilter = medicine =>
                medicine.Name.ToLower().Contains(nameCriteria) &&
                medicine.Stock > 0;

            return _medicineRepository.GetAllByExpression(medicineFilter).ToList();
        }

        public Medicine GetSpecificMedicine(int medicineId)
        {
            var medicineSaved = _medicineRepository.GetOneByExpression(i => i.Id == medicineId);

            if (medicineSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified medicine {medicineId}");
            }

            return medicineSaved;
        }

        public Medicine CreateMedicine(Medicine medicine)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));

            medicine.IsValidMedicine();
            IsMedicineRegistered(medicine);

            pharmacy.Stock.Add(medicine);
            ModifyRecords(pharmacy, medicine);

            return medicine;
        }

        public Medicine UpdateMedicine(int medicineId, Medicine updatedMedicine)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));

            updatedMedicine.IsValidMedicine();

            var medicineStored = GetSpecificMedicine(medicineId);

            if (!pharmacy.Stock.Contains(medicineStored))
            {
                throw new InputException("The drug you want to modify does not belong to your pharmacy");
            }

            SetValues(medicineStored, updatedMedicine);

            _medicineRepository.UpdateOne(medicineStored);
            _medicineRepository.Save();

            return medicineStored;
        }

        public void DeleteMedicine(int medicineId)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));

            var medicineStored = GetSpecificMedicine(medicineId);

            if (pharmacy.Stock.Contains(medicineStored))
            {
                pharmacy.Stock.Remove(medicineStored);
                ModifyRecords(pharmacy, medicineStored);
            }
            else
            {
                throw new InputException("The drug you want to delete does not belong to your pharmacy");
            }
        }

        private void SetValues(Medicine medicineStored, Medicine updatedMedicine)
        {
            medicineStored.Measure = updatedMedicine.Measure;
            medicineStored.Price = updatedMedicine.Price;
            medicineStored.Code = updatedMedicine.Code;
            medicineStored.Stock = updatedMedicine.Stock;
            medicineStored.Amount = updatedMedicine.Amount;
            medicineStored.Symptoms = updatedMedicine.Symptoms;
            medicineStored.Prescription = updatedMedicine.Prescription;
            medicineStored.Presentation = updatedMedicine.Presentation;
            medicineStored.Name = updatedMedicine.Name;
        }

        private void IsMedicineRegistered(Medicine medicine)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));

            if (pharmacy.Stock.Contains(medicine))
            {
                throw new InputException("The medicine with that code already exist");
            }
        }

        private void ModifyRecords(Pharmacy pharmacy, Medicine medicine)
        {
            _pharmacyRepository.UpdateOne(pharmacy);
            _pharmacyRepository.Save();
            _medicineRepository.DeleteOne(medicine);
            _medicineRepository.Save();
        }
    }
}
