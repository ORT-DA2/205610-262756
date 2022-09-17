using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IInvoiceLineManager
    {
        List<InvoiceLine> GetAllInvoiceLine(InvoiceLineSearchCriteria searchCriteria);
        InvoiceLine GetSpecificInvoiceLine(Medicine medicine);
        InvoiceLine CreateInvoiceLine(InvoiceLine invoiceLine);
        InvoiceLine UpdateInvoiceLine(int amount, InvoiceLine invoiceLine);
        InvoiceLine DeleteInvoiceLine(InvoiceLine invoiceLine);
    }
}
