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
            var amountCriteria = searchCriteria.Amount?.ToString().ToLower() ?? string.Empty;
            var presentationCriteria = searchCriteria.Presentation?.ToLower() ?? string.Empty;
            var priceCriteria = searchCriteria.Price.ToString() ?? string.Empty;
            var measureCriteria = searchCriteria.Measure?.ToString() ?? string.Empty;
            var codeCriteria = searchCriteria.Code.ToLower() ?? string.Empty;
            var prescriptionCriteria = searchCriteria.Prescription?.ToString() ?? string.Empty;
            var stockCriteria = searchCriteria.Stock?.ToString() ?? string.Empty;

            Expression<Func<Medicine, bool>> medicineFilter = medicine =>
                medicine.Name.ToLower().Contains(nameCriteria) &&
                medicine.Amount.ToString().ToLower().Contains(amountCriteria) &&
                medicine.Code.ToString().Contains(codeCriteria) &&
                medicine.Measure.ToLower().Contains(measureCriteria) &&
                medicine.Price.ToString().Contains(priceCriteria) &&
                medicine.Prescription.ToString().Contains(prescriptionCriteria) &&
                medicine.Presentation.ToLower().Contains(presentationCriteria) &&
                medicine.Stock.ToString().Contains(stockCriteria);

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
            medicine.isValidMedicine();

            _medicineRepository.InsertOne(medicine);
            _medicineRepository.Save();

            return medicine;
        }

        public Medicine UpdateMedicine(int medicineId, Medicine updatedMedicine)
        {
            updatedMedicine.isValidMedicine();

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
    }
}
