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
    public class SaleManager : ISaleManager
    {
        private readonly IRepository<Sale> _saleRepository;

        public SaleManager(IRepository<Sale> saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public List<Sale> GetAllSale(SaleSearchCriteria searchCriteria)
        {
            var medicinesCriteria = searchCriteria.InvoiceLines.ToList() ?? null;

            Expression<Func<Sale, bool>> saleFilter = sale =>
                sale.InvoiceLines.ToList() == medicinesCriteria;

            return _saleRepository.GetAllExpression(saleFilter).ToList();
        }

        public Sale GetSpecificSale(int saleId)
        {
            var saleSaved = _saleRepository.GetOneByExpression(s => s.Id == saleId);

            if (saleSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified sale {saleId}");
            }
            return saleSaved;
        }

        public Sale CreateSale(Sale sale)
        {
            sale.isValidSale();

            _saleRepository.InsertOne(sale);
            _saleRepository.Save();

            return sale;
        }

        public Sale UpdateSale(int saleId, Sale updatedSale)
        {
            updatedSale.isValidSale();

            var saleStored = GetSpecificSale(saleId);

            saleStored.InvoiceLines = updatedSale.InvoiceLines;

            _saleRepository.UpdateOne(saleStored);
            _saleRepository.Save();

            return saleStored;
        }

        public void DeleteSale(int saleId)
        {
            var saleStored = GetSpecificSale(saleId);

            _saleRepository.DeleteOne(saleStored);
            _saleRepository.Save();
        }
    }
}
