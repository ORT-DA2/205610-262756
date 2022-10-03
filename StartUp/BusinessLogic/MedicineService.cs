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

        public MedicineService(IRepository<Medicine> medicineRepository)
        {
            _medicineRepository = medicineRepository;
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
            medicine.IsValidMedicine();
            IsMedicineRegistered(medicine);
            
            _medicineRepository.InsertOne(medicine);
            _medicineRepository.Save();

            return medicine;
        }

        public Medicine UpdateMedicine(int medicineId, Medicine updatedMedicine)
        {
            updatedMedicine.IsValidMedicine();

            var medicineStored = GetSpecificMedicine(medicineId);

            medicineStored.Measure = updatedMedicine.Measure;
            medicineStored.Price = updatedMedicine.Price;
            medicineStored.Code = updatedMedicine.Code;
            medicineStored.Stock = updatedMedicine.Stock;
            medicineStored.Amount = updatedMedicine.Amount;
            medicineStored.Symptoms = updatedMedicine.Symptoms;
            medicineStored.Prescription = updatedMedicine.Prescription;
            medicineStored.Presentation = updatedMedicine.Presentation;
            medicineStored.Name = updatedMedicine.Name;

            _medicineRepository.UpdateOne(medicineStored);
            _medicineRepository.Save();

            return medicineStored;
        }

        public void DeleteMedicine(int medicineId)
        {
            var medicineStored = GetSpecificMedicine(medicineId);

            _medicineRepository.DeleteOne(medicineStored);
            _medicineRepository.Save();
        }

        private void IsMedicineRegistered(Medicine medicine)
        {
            var med = _medicineRepository.GetOneByExpression(m => m.Code == medicine.Code);

            if (med != null)
            {
                throw new InputException("The medicine with that code already exist");
            }
        }
    }
}
