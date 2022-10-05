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

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewSaleWithInvoiceLinesNullTest()
        {
            Sale sale = CreateSale(1, null);

            sale.isValidSale();
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void CreateSaleDoubleTest()
        {
            Sale sale = CreateSale(1, lines);

            sale.isValidSale();

            Sale sale2 = CreateSale(1, lines);

            sale2.isValidSale();
        }

        [TestMethod]
        public void CompareTwoSalesTest()
        {
            Sale sale = CreateSale(1, lines);
            Sale sale1 = CreateSale(1, lines);

            bool areEqual = sale.Equals(sale1);

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void CompareTwoDiferentSaleTest()
        {
            Sale sale = CreateSale(1, lines);
            Sale sale1 = CreateSale(2, lines);

            bool areEqual = sale.Equals(sale1);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void CompareNullSaleTest()
        {
            Sale sale = CreateSale(1, lines);
            Sale sale1 = null;

            bool areEqual = sale.Equals(sale1);

            Assert.IsFalse(areEqual);
        }

        private Sale CreateSale(int id, List<InvoiceLine> invoiceLine)
        {
            return new Sale()
            {
                Id = id,
                InvoiceLines = invoiceLine,
            };
        }
    }
}
