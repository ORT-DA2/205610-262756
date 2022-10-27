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
    public class InvoiceLineService : IInvoiceLineService
    {
        private readonly IRepository<InvoiceLine> _invoiceLineRepository;
        private readonly IRepository<Medicine> _medicineRepository;
        private readonly IRepository<Pharmacy> _pharmacyRepository;
        private readonly ISessionService _sessionService;

        public InvoiceLineService(IRepository<InvoiceLine> invoiceLineRepository, 
            IRepository<Medicine> medicineRepository, IRepository<Pharmacy> pharmacyRepository, 
            ISessionService sessionService)
        {
            _invoiceLineRepository = invoiceLineRepository;
            _medicineRepository = medicineRepository;
            _pharmacyRepository = pharmacyRepository;
            _sessionService = sessionService;
        }

        public List<InvoiceLine> GetAllInvoiceLine(InvoiceLineSearchCriteria searchCriteria)
        {
            var medicineMeasureCriteria = searchCriteria.Medicine?.Measure?.ToLower() ?? string.Empty;
            var amountCriteria = searchCriteria.Amount?.ToString() ?? string.Empty;

            Expression<Func<InvoiceLine, bool>> invoiceLineFilter = invoiceLine =>
                invoiceLine.Medicine.Measure.ToLower().Contains(medicineMeasureCriteria) &&
                invoiceLine.Amount.ToString().ToLower().Contains(amountCriteria);

            return _invoiceLineRepository.GetAllByExpression(invoiceLineFilter).ToList();
        }

        public InvoiceLine GetSpecificInvoiceLine(int invoiceLineId)
        {
            var invoiceLineSaved = _invoiceLineRepository.GetOneByExpression(
                i => i.Medicine.Id == invoiceLineId);

            if (invoiceLineSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified invoiceLine {invoiceLineId}");
            }

            return invoiceLineSaved;
        }

        public InvoiceLine CreateInvoiceLine(InvoiceLine invoiceLine)
        {
            invoiceLine.IsValidInvoiceLine();
            invoiceLine.Medicine = _medicineRepository.GetOneByExpression(m => m.Code == invoiceLine.Medicine.Code);
            invoiceLine.State = "Pending";

            _invoiceLineRepository.InsertOne(invoiceLine);
            _invoiceLineRepository.Save();

            return invoiceLine;
        }

        public InvoiceLine UpdateInvoiceLine(int invoiceLineId, InvoiceLine updatedInvoiceLine)
        {
            updatedInvoiceLine.IsValidInvoiceLine();

            var invoiceLineStored = GetSpecificInvoiceLine(invoiceLineId);

            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));

            if (invoiceLineStored.Pharmacy == pharmacy)
            {
                invoiceLineStored.Medicine = _medicineRepository.GetOneByExpression(m => m.Code == updatedInvoiceLine.Medicine.Code);
                invoiceLineStored.Amount = updatedInvoiceLine.Amount;
            }
            else
            {
                throw new InputException("The bill line does not belong to your pharmacy");
            }

            _invoiceLineRepository.UpdateOne(invoiceLineStored);
            _invoiceLineRepository.Save();

            return invoiceLineStored;
        }

        public void DeleteInvoiceLine(int invoiceLineId)
        {
            var invoiceLineStored = GetSpecificInvoiceLine(invoiceLineId);

            _invoiceLineRepository.DeleteOne(invoiceLineStored);
            _invoiceLineRepository.Save();
        }
    }
}
