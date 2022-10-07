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
    public class SaleRepositoryTest
    {
        private BaseRepository<Sale> _repository;
        private StartUpContext _context;
        private SaleRepository _saleRepository;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Sale>(_context);
            _saleRepository = new SaleRepository(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void InsertNewSale()
        {
            var sales = CreateSale();
            LoadSale(sales);
            var newSale = new Sale()
            {
                InvoiceLines = new List<InvoiceLine>()
            };

            _repository.InsertOne(newSale);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var saleInDb = _context.Sales.FirstOrDefault(s => s.Id.Equals(newSale.Id));
            Assert.IsNotNull(saleInDb);
        }
        
        [TestMethod]
        public void GetOneByExpressionNotExistTest()
        {
            InvoiceLine invoice = new InvoiceLine
            {
                  Amount = 1
            };
            Expression<Func<Sale, bool>> expression = s => s.InvoiceLines.Equals(invoice);

            var retrievedSale = _saleRepository.GetOneByExpression(expression);
            
            Assert.IsNull(retrievedSale);
        }
        
        [TestMethod]
        public void GetOneByExpressionTest()
        {
            Sale newSale = CreateSale();
            InvoiceLine invoice = new InvoiceLine
            {
                Amount = 4,
                Medicine = new Medicine(),
            };
            Expression<Func<Sale, bool>> expression = s => s.InvoiceLines.Equals(invoice);
            LoadSale(newSale);
            
            var retrievedSale = _saleRepository.GetOneByExpression(expression);
            
            Assert.IsNotNull(retrievedSale);
        }


        private void LoadSale(Sale sale)
        {
            _context.InvoiceLines.Add(sale.InvoiceLines[0]);
            _context.Sales.Add(sale);
            _context.SaveChanges();
        }

        private Sale CreateSale()
        {
            InvoiceLine invoice = new InvoiceLine
            {
                Amount = 4,
                Medicine = new Medicine(),
            };
            Sale sale = new Sale();
            sale.InvoiceLines = new List<InvoiceLine>();
            sale.InvoiceLines.Add(invoice);
            return sale;
        }
    }
}
