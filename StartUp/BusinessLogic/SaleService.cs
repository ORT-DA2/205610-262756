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
    public class SaleService : ISaleService
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly ISessionService _sessionService;
        private readonly IRepository<Pharmacy> _pharmacyRepository;


        public SaleService(IRepository<Sale> saleRepository, ISessionService sessionService, IRepository<Pharmacy> pharmacyRepository)
        {
            _saleRepository = saleRepository;
            _sessionService = sessionService;
            _pharmacyRepository = pharmacyRepository;
        }

        public List<Sale> GetAllSale()
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            return pharmacy.Sales;
        }

        public Sale GetSpecificSale(int saleId)
        {
            Validator validator = new Validator();
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            var saleSaved = _saleRepository.GetOneByExpression(s => s.Id == saleId);
            validator.ValidateSaleNotNull(saleSaved, $"Could not find specified sale {saleId}");

            if (pharmacy.Sales.Contains(saleSaved))
            {
                return saleSaved;
            }
            else
            {
                throw new InvalidResourceException("The sale you want to see does not belong to your pharmacy");
            }
        }

        public Sale CreateSale(Sale sale)
        {
            sale.isValidSale();

            List<Pharmacy> list = AllMedicationsCorrespondToTheSamePharmacy(sale);

            list = CheckDrugStock(list, sale);
            if (list.Count == 0)
            {
                throw new InvalidResourceException("No pharmacy has such amounts of medicine stock");
            }

            Pharmacy pharmacy = list.FirstOrDefault();
            pharmacy.Sales.Add(sale);
            pharmacy.UpdateStock(sale);
            _pharmacyRepository.UpdateOne(pharmacy);
            _pharmacyRepository.Save();
            _saleRepository.InsertOne(sale);
            _saleRepository.Save();

            return sale;
        }

        private List<Pharmacy> CheckDrugStock(List<Pharmacy> list, Sale sale)
        {
            foreach (Pharmacy pharmacy in list)
            {
                foreach (Medicine medicine in pharmacy.Stock)
                {
                    foreach (InvoiceLine line in sale.InvoiceLines)
                    {
                        if (line.Medicine == medicine)
                        {
                            if (line.Amount > medicine.Stock)
                            {
                                list.Remove(pharmacy);
                            }
                        }
                    }

                }
            }
            return list;
        }

        private List<Pharmacy> AllMedicationsCorrespondToTheSamePharmacy(Sale sale)
        {
            Validator validator = new Validator();
            var invoiceLineFirst = sale.InvoiceLines.FirstOrDefault();
            Medicine medicine = invoiceLineFirst.Medicine;

            List <Pharmacy> list = _pharmacyRepository.GetAllByExpression(p=>p.Stock.Contains(medicine)).ToList();
            validator.ValidateListPharmacyNotNull(list, "No pharmacy has all those drugs");

            foreach (InvoiceLine item in sale.InvoiceLines)
            {
                Medicine medicineItem = item.Medicine;
                Expression<Func<Pharmacy, bool>> pharmacyFilter2 =
                pharmacy => pharmacy.Stock.Contains(medicineItem);

                List<Pharmacy> listItem = _pharmacyRepository.GetAllByExpression(pharmacyFilter2).ToList();
                validator.ValidateListPharmacyNotNull(listItem, $"No pharmacy has this medicine {medicineItem.Name}");

                if (list != listItem)
                {
                    list = GetPharmaciesInCommon(list, listItem);
                }
            }
            return list;
        }

        private List<Pharmacy> GetPharmaciesInCommon(List<Pharmacy> list, List<Pharmacy> listItem)
        {
            List<Pharmacy> pharmacies = new List<Pharmacy>();

            foreach (Pharmacy pharmacy in list)
            {
                foreach (Pharmacy pharmacy2 in listItem)
                {
                    if (pharmacy == pharmacy2)
                    {
                        pharmacies.Add(pharmacy);
                    }
                }
            }
            if (pharmacies.Count == 0)
            {
                throw new InvalidResourceException("Not all medications belong to the same pharmacy");
            }
            else
            {
                return pharmacies;
            }
        }
    }
}
