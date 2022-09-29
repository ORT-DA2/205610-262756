using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class SaleRepositoryTest
    {
        private BaseRepository<Sale> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Sale>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllSaleReturnsAsExpected()
        {
            Expression<Func<Sale, bool>> expression = s => s.Id.ToString().Contains("1");
            var sales = CreateSales();
            var eligibleSales = sales.Where(expression.Compile()).ToList();
            LoadSales(sales);

            var retrievedSales = _repository.GetAllExpression(expression);
            CollectionAssert.AreEquivalent(eligibleSales, retrievedSales.ToList());
        }

        [TestMethod]
        public void InsertNewSale()
        {
            var sales = CreateSales();
            LoadSales(sales);
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


        private void LoadSales(List<Sale> sale)
        {
            sale.ForEach(s => _context.Sales.Add(s));
            _context.SaveChanges();
        }

        private List<Sale> CreateSales()
        {
            return new List<Sale>()
        {
            new()
            {
               InvoiceLines = new List<InvoiceLine>()
            },
            new()
            {
                InvoiceLines = new List<InvoiceLine>()
            }
        };
        }
    }
}
