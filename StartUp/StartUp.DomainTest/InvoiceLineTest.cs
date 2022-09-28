
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;

namespace StartUp.DomainTest
{
    [TestClass]
    public class InvoiceLineTest
    {
        [TestMethod]
        public void ValidOrFailPassesWithValidAdministrator()
        {
            InvoiceLine invoiceLine = new InvoiceLine();
            invoiceLine.isValidInvoiceLine();
        }
    }
}
