using StartUp.Domain;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StartUp.BusinessLogic
{
    public class SaleService : ISaleService
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly ISessionService _sessionService;
        private readonly IRepository<Pharmacy> _pharmacyRepository;
        private readonly IRepository<InvoiceLine> _invoiceLineRepository;
        private readonly IRepository<Medicine> _medicineRepository;


        public SaleService(IRepository<Sale> saleRepository, ISessionService sessionService,
            IRepository<Pharmacy> pharmacyRepository, IRepository<InvoiceLine> invoiceLineRepository,
            IRepository<Medicine> medicineRepository)
        {
            _saleRepository = saleRepository;
            _sessionService = sessionService;
            _pharmacyRepository = pharmacyRepository;
            _invoiceLineRepository = invoiceLineRepository;
            _medicineRepository = medicineRepository;
        }

        public List<Sale> GetAllSale()
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            List<Sale> list = FilterSalesByInvoiceLines(pharmacy);

            return list;
        }

        private List<Sale> FilterSalesByInvoiceLines(Pharmacy pharmacy)
        {
            List<Sale> list = new List<Sale>();

            foreach (Sale sale in pharmacy.Sales)
            {
                Sale newSale = new Sale();
                newSale.InvoiceLines = new List<InvoiceLine>();
                Sale restoredSale = _saleRepository.GetOneByExpression(s => s.Code == sale.Code);
                newSale.Code = restoredSale.Code;

                foreach (InvoiceLine item in restoredSale.InvoiceLines)
                {
                    if (item.PharmacyId == pharmacy.Id)
                    {
                        newSale.InvoiceLines.Add(item);
                    }
                }
                list.Add(newSale);
            }
            return list;
        }

        public Sale GetSpecificSale(int saleId)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            var saleSaved = _saleRepository.GetOneByExpression(s => s.Id == saleId);
            if(saleSaved == null)
            {
                throw new InputException($"Could not find specified sale {saleId}");
            }

            if (pharmacy.Sales.Contains(saleSaved))
            {
                return saleSaved;
            }
            else
            {
                throw new InvalidResourceException("The sale you want to see does not belong to your pharmacy");
            }
        }

        public Sale GetSpecificSaleForCode(string saleCode)
        {

            saleCode = CleanString(saleCode);
            Sale saleSaved = _saleRepository.GetOneByExpression(s => s.Code == Int32.Parse(saleCode));
            if (saleSaved == null)
            {
                throw new InputException($"Could not find specified sale {saleCode}");
            }

            return saleSaved;
        }

        private string CleanString(string saleCode)
        {
            string clean = "";
            int i = 1;
            while (i < saleCode.Length - 1)
            {
                clean = clean + saleCode[i];
                i++;
            }
            return clean;
        }

        public Sale CreateSale(Sale sale)
        {
            sale.isValidSale();
            if (_sessionService.UserLogged == null)
            {
                List<Pharmacy> list = PharmaciesThatHaveTheDrugsAndQuantitiesRequested(sale, false);
                list = PharmaciesThatHaveTheDrugsAndQuantitiesRequested(sale, true);

                sale.Code = GenerateCode();

                _saleRepository.InsertOne(sale);
                _saleRepository.Save();

                AddSaleToPharmacies(list, sale);
                return sale;
            }
            else
            {
                throw new InputException("Users who are logged into the system cannot make purchases");
            }
        }

        public Sale UpdateSale(int saleCode, Sale updatedSale)
        {
            updatedSale.isValidSale();
            updatedSale.ValidateState();
            bool once = false;

            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            List<Sale> salesStored = _saleRepository.GetAllByExpression(s => s.Code == saleCode).ToList();

            foreach (Sale sale in salesStored)
            {
                Sale restoredSale = _saleRepository.GetOneByExpression(s => s.Id == sale.Id);

                foreach (InvoiceLine line in restoredSale.InvoiceLines)
                {
                    InvoiceLine lineSaleInBD = _invoiceLineRepository.GetOneByExpression(i => i.Id == line.Id);

                    if (lineSaleInBD.PharmacyId == pharmacy.Id && lineSaleInBD.State == "Pending")
                    {
                        foreach (InvoiceLine inv in updatedSale.InvoiceLines)
                        {

                            if (lineSaleInBD.Medicine.Name == inv.Medicine.Name && lineSaleInBD.Amount == inv.Amount)
                            {
                                lineSaleInBD.State = inv.State;
                                if (lineSaleInBD.State == "approved" && !once)
                                {
                                    UpdateStockPharmacy(pharmacy, inv);
                                    once = true;
                                }
                                _invoiceLineRepository.UpdateOne(lineSaleInBD);
                                _invoiceLineRepository.Save();
                            }

                        }
                    }
                }
                _saleRepository.UpdateOne(restoredSale);
                _saleRepository.Save();
            }
            return _saleRepository.GetOneByExpression(s => s.Code == saleCode);
        }

        private void UpdateStockPharmacy(Pharmacy pharmacy, InvoiceLine inv)
        {
            foreach (Medicine medicine in pharmacy.Stock)
            {
                if (medicine.Code == inv.Medicine.Code)
                {
                    medicine.Stock = medicine.Stock - inv.Amount;
                    _medicineRepository.UpdateOne(medicine);
                    _medicineRepository.Save();
                }
            }
            _pharmacyRepository.UpdateOne(pharmacy);
            _pharmacyRepository.Save();
        }

        private void AddSaleToPharmacies(List<Pharmacy> list, Sale sale)
        {
            bool once = false;

            foreach (Pharmacy pharmacy in list)
            {
                Sale restoredSale = _saleRepository.GetOneByExpression(s => s.Code == sale.Code);
                Pharmacy restoredPharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == pharmacy.Name);

                if (once)
                {
                    Sale copy = CreateCopySale(restoredSale);
                    restoredPharmacy.Sales.Add(copy);
                }
                else
                {
                    restoredPharmacy.Sales.Add(restoredSale);
                    once = true;
                }
                _pharmacyRepository.UpdateOne(restoredPharmacy);
                _pharmacyRepository.Save();
            }
        }

        private Sale CreateCopySale(Sale restoredSale)
        {
            Sale copy = new Sale();
            CopyAndSaveInvoiceLines(restoredSale, copy);

            copy.Code = restoredSale.Code;

            _saleRepository.InsertOne(copy);
            _saleRepository.Save();

            return copy;
        }

        private void CopyAndSaveInvoiceLines(Sale restoredSale, Sale copy)
        {
            copy.InvoiceLines = new List<InvoiceLine>();

            foreach (InvoiceLine line in restoredSale.InvoiceLines)
            {
                InvoiceLine invCopy = new InvoiceLine();
                invCopy.Medicine = line.Medicine;
                invCopy.Amount = line.Amount;
                invCopy.State = line.State;
                invCopy.PharmacyId = line.PharmacyId;
                copy.InvoiceLines.Add(invCopy);

                _invoiceLineRepository.InsertOne(invCopy);
                _invoiceLineRepository.Save();
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
            List<Pharmacy> pharmacys = _pharmacyRepository.GetAllByExpression(p => p.Stock.Count > 0).ToList();
            pharmacys = FilterPharmacy(pharmacys, item.Medicine);
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
                        SaveChangesInvoiceLine(item, p, medicine);
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

        private List<Pharmacy> FilterPharmacy(List<Pharmacy> pharmacys, Medicine medicineItem)
        {
            List<Pharmacy> list = new List<Pharmacy>();

            foreach (Pharmacy pharmacy in pharmacys)
            {
                foreach (Medicine medicine in pharmacy.Stock)
                {
                    if (medicine.Name == medicineItem.Name)
                    {
                        list.Add(pharmacy);
                    }
                }
            }
            return list;
        }

        private void SaveChangesInvoiceLine(InvoiceLine item, Pharmacy p, Medicine medicineFromPharmacy)
        {
            Medicine medicine = _medicineRepository.GetOneByExpression(m => m.Name == medicineFromPharmacy.Name
            && m.Code == medicineFromPharmacy.Code);
            item.Medicine = medicine;
            item.PharmacyId = p.Id;
            item.State = "pending";
            _invoiceLineRepository.InsertOne(item);
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
                    if (item.PharmacyId != pharmacy.Id)
                    {
                        sale.InvoiceLines.Remove(item);
                    }
                }
            }
            return sale;
        }
    }
}