using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IInvoiceLineService
    {
        List<InvoiceLine> GetAllInvoiceLine(InvoiceLineSearchCriteria searchCriteria);
        InvoiceLine GetSpecificInvoiceLine(int invoiceLineId);
        InvoiceLine CreateInvoiceLine(InvoiceLine invoiceLine);
        InvoiceLine UpdateInvoiceLine(int invoiceLineId, InvoiceLine invoiceLine);
        void DeleteInvoiceLine(int invoiceLineId);
    }
}
