
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;

namespace StartUp.DomainTest
{
    [TestClass]
    public class InvoiceLineTest
    {

        private Medicine medicine;
        
        [TestInitialize]
        public void Setup()
        {
            medicine = new Medicine();
        }
        
        [TestMethod]
        public void NewInvoiceLineTest()
        {
            InvoiceLine invoiceLine = CreateInvoiceLine(1, 3, medicine);

            invoiceLine.IsValidInvoiceLine();
            
            Assert.IsNotNull(invoiceLine);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewInvoiceLineWithMedicineNullTest()
        {
            InvoiceLine invoiceLine = CreateInvoiceLine(1, 3, null);

            invoiceLine.IsValidInvoiceLine();
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exceptions.InputException))]
        public void NewInvoiceLineWithAmount0Test()
        {
            InvoiceLine invoiceLine = CreateInvoiceLine(1, 0, medicine);

            invoiceLine.IsValidInvoiceLine();
        }

        [TestMethod]
        public void CompareNullInvoiceLineTest()
        {
            InvoiceLine invoiceLine = CreateInvoiceLine(1, 3, medicine);

            InvoiceLine invoiceLine1 = null;

            bool areEqual = invoiceLine.Equals(invoiceLine1);
            
            Assert.IsFalse(areEqual);
        }
        
        [TestMethod]
        public void CompareEqualCodeInvoiceLineTest()
        {
            InvoiceLine invoiceLine = CreateInvoiceLine(1, 3, medicine);

            InvoiceLine invoiceLine1 = CreateInvoiceLine(1, 3, medicine);

            bool areEqual = invoiceLine.Equals(invoiceLine1);
            
            Assert.IsTrue(areEqual);
        }
        
        [TestMethod]
        public void CompareDifferentInvoiceLineTest()
        {
            InvoiceLine invoiceLine = CreateInvoiceLine(1, 3, medicine);

            InvoiceLine invoiceLine1 = CreateInvoiceLine(2, 3, medicine);

            bool areEqual = invoiceLine.Equals(invoiceLine1);
            
            Assert.IsFalse(areEqual);
        }
        
        private InvoiceLine CreateInvoiceLine(int id, int amount, Medicine med)
        {
            return new InvoiceLine()
            {
                Id = id,
                Amount = amount,
                Medicine = med
            };
        }

    }
}
