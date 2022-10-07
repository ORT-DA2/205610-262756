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
using StartUp.DataAccess.Repositories;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class InvoiceLineRepositoryTest
    {
        private BaseRepository<InvoiceLine> _repository;
        private InvoiceLineRepository _invoiceLineRepository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<InvoiceLine>(_context);
            _invoiceLineRepository = new InvoiceLineRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            Expression<Func<InvoiceLine, bool>> expression = i => i.Medicine.Name.ToLower().Contains("Clonapine");
            var invoiceLine = CreateInvoiceLines();

            var retrievedInvoiceLines = _invoiceLineRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedInvoiceLines);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            InvoiceLine newInvoiceLine = CreateInvoiceLines();
            Expression<Func<InvoiceLine, bool>> expression = i => i.Medicine.Name.ToLower().Contains("perifar");
            LoadInvoiceLines(newInvoiceLine);
            
            var retrievedInvoiceLines = _invoiceLineRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedInvoiceLines);
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


        private void LoadInvoiceLines(InvoiceLine invoiceLine)
        {
            _context.InvoiceLines.Add(invoiceLine);
            _context.SaveChanges();
        }

        private InvoiceLine CreateInvoiceLines()
        {
            return new()
            {
                Amount = 100,
                Medicine = new Medicine
                {
                    Name = "Perifar"
                }
            };
        }
    }
}
