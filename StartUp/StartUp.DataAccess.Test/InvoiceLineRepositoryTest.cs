using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StartUp.DataAccess.Contexts;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class InvoiceLineRepositoryTest
    {
        private BaseRepository<InvoiceLine> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<InvoiceLine>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllInvoiceLineReturnsAsExpected()
        {
            Expression<Func<InvoiceLine, bool>> expression = i => i.Medicine.Name.ToLower().Contains("perifar");
            var invoiceLines = CreateInvoiceLines();
            var eligibleInvoiceLines = invoiceLines.Where(expression.Compile()).ToList();
            LoadInvoiceLines(invoiceLines);

            var retrievedInvoiceLines = _repository.GetAllByExpression(expression);
            CollectionAssert.AreEquivalent(eligibleInvoiceLines, retrievedInvoiceLines.ToList());
        }

        [TestMethod]
        public void InsertNewInvoiceLine()
        {
            var invoiceLines = CreateInvoiceLines();
            LoadInvoiceLines(invoiceLines);
            var newInvoiceLine = new InvoiceLine()
            {
                Amount = 100,
                Medicine = new Medicine(),
            };

            _repository.InsertOne(newInvoiceLine);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var invoiceLineInDb = _context.InvoiceLines.FirstOrDefault(i => i.Medicine.Name.Equals(newInvoiceLine.Medicine.Name));
            Assert.IsNotNull(invoiceLineInDb);
        }


        private void LoadInvoiceLines(List<InvoiceLine> invoiceLines)
        {
            invoiceLines.ForEach(i => _context.InvoiceLines.Add(i));
            _context.SaveChanges();
        }

        private List<InvoiceLine> CreateInvoiceLines()
        {
            return new List<InvoiceLine>()
        {
            new()
            {
               Amount = 100,
                Medicine = new Medicine()
            },
            new()
            {
                Amount = 100,
                Medicine = new Medicine()
            }
        };
        }
    }
}
