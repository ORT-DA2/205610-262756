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
        private readonly IRepository<InvoiceLine> _invoiceLineRepository;


        public SaleService(IRepository<Sale> saleRepository, ISessionService sessionService,
            IRepository<Pharmacy> pharmacyRepository, IRepository<InvoiceLine> invoiceLineRepository)
        {
            _saleRepository = saleRepository;
            _sessionService = sessionService;
            _pharmacyRepository = pharmacyRepository;
            _invoiceLineRepository = invoiceLineRepository;
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

            List<Pharmacy> list = PharmaciesThatHaveTheDrugsAndQuantitiesRequested(sale, false);
            list = PharmaciesThatHaveTheDrugsAndQuantitiesRequested(sale, true);

            AddSaleToPharmacies(list, sale);
            sale.Code = GenerateCode();

            _saleRepository.InsertOne(sale);
            _saleRepository.Save();

            return sale;
        }

        public Sale UpdateSale(int saleId, Sale updatedSale)
        {
            updatedSale.isValidSale();

            var saleStored = GetSpecificSale(saleId);

            saleStored = updatedSale;

            _saleRepository.UpdateOne(saleStored);
            _saleRepository.Save();

            return saleStored;
        }

        private void AddSaleToPharmacies(List<Pharmacy> list, Sale sale)
        {
            foreach (Pharmacy pharmacy in list)
            {
                pharmacy.Sales.Add(sale);
                _pharmacyRepository.UpdateOne(pharmacy);
                _pharmacyRepository.Save();
            }
        }

        private List<Pharmacy> PharmaciesThatHaveTheDrugsAndQuantitiesRequested(Sale sale, bool save)
        {
            List<Pharmacy> list = new List<Pharmacy>();

            foreach (InvoiceLine item in sale.InvoiceLines)
            {
                list = AddOrRemovePharmacy(item, list, save);
            }
            return list;
        }

        private List<Pharmacy> AddOrRemovePharmacy(InvoiceLine item, List<Pharmacy> list, bool save)
        {
            List<Pharmacy> pharmacys = _pharmacyRepository.GetAllByExpression(p => p.Stock.Contains(item.Medicine)).ToList();
            bool ThereIsNot = false;
            while (!ThereIsNot && pharmacys.Count > 0)
            {
                Pharmacy p = pharmacys.FirstOrDefault();
                Medicine medicine = p.Stock.Where(m => m.Name == item.Medicine.Name).FirstOrDefault();
                if (medicine.Stock >= item.Amount)
                {
                    list.Add(p);
                    ThereIsNot = true;
                    if (save)
                    {
                        SaveChangesInvoiceLine(item, p);
                    }
                }
                else
                {
                    pharmacys.Remove(p);
                }
            }
            if (!ThereIsNot)
            {
                throw new InvalidResourceException($"No pharmacy has {item.Amount} medicines {item.Medicine.Name}");
            }
            return list;
        }

        private void SaveChangesInvoiceLine(InvoiceLine item, Pharmacy p)
        {
            item.Pharmacy = p;
            item.State = "pending";
            _invoiceLineRepository.UpdateOne(item);
            _invoiceLineRepository.Save();
        }

        private int GenerateCode()
        {
            Random random = new Random();
            int code = random.Next(1000000, 9999999);
            Sale sale = _saleRepository.GetOneByExpression(c => code == c.Code);
            if (sale != null)
            {
                return GenerateCode();
            }
            return code;
        }

        public Sale FilterByPharmacy(Sale sale)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);
            
            if (pharmacy != null)
            {
                foreach (var item in sale.InvoiceLines)
                {
                    if (item.Pharmacy != pharmacy)
                    {
                        sale.InvoiceLines.Remove(item);
                    }
                }
            }
            return sale;
        }
    }
}