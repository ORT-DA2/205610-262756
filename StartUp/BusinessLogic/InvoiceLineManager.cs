using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class InvoiceLineManager : IInvoiceLineManager
    {
        public InvoiceLine CreateInvoiceLine()
        {
            throw new NotImplementedException();
        }

        public InvoiceLine CreateInvoiceLine(InvoiceLine invoiceLine)
        {
            throw new NotImplementedException();
        }

        public InvoiceLine DeleteInvoiceLine(InvoiceLine invoiceLine)
        {
            throw new NotImplementedException();
        }

        public List<InvoiceLine> GetAllInvoiceLine(InvoiceLineSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public InvoiceLine GetSpecificInvoiceLine()
        {
            throw new NotImplementedException();
        }

        public InvoiceLine GetSpecificInvoiceLine(Medicine medicine)
        {
            throw new NotImplementedException();
        }

        public InvoiceLine UpdateInvoiceLine(int amount, InvoiceLine invoiceLine)
        {
            throw new NotImplementedException();
        }
    }
}
