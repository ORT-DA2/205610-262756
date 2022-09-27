using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DomainTest
{
    [TestClass]
    public class SaleTest
    {
        List<InvoiceLine> lines;

        [TestInitialize]
        public void Setup()
        {
            lines = new List<InvoiceLine>();
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        [TestMethod]
        public void NewSaleTestOK()
        {
            Sale sale = new Sale();
            sale.InvoiceLines = lines;

            Assert.IsNotNull(sale);
        }
    }
}
