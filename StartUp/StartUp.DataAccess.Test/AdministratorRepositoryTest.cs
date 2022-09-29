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
    public class AdministratorRepositoryTest
    {
        private BaseRepository<Administrator> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Administrator>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllAdministratorReturnsAsExpected()
        {
            Expression<Func<Administrator, bool>> expression = a => a.Email.ToLower().Contains("paulaolivera1995@gmail.com");
            var administrators = CreateAdministrators();
            var eligibleAdministrators = administrators.Where(expression.Compile()).ToList();
            LoadAdministrators(administrators);

            var retrievedAdministrators = _repository.GetAllExpression(expression);
            CollectionAssert.AreEquivalent(eligibleAdministrators, retrievedAdministrators.ToList());
        }

        [TestMethod]
        public void InsertNewAdministrator()
        {
            var administrators = CreateAdministrators();
            LoadAdministrators(administrators);
            var newAdministrator = new Administrator()
            {
                Email = "paulaolivera1995@gmail.com",
                Address = "carlos maria ramirez",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "123ss",
            };

            _repository.InsertOne(newAdministrator);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var administratorInDb = _context.Administrators.FirstOrDefault(a => a.Email.Equals(newAdministrator.Email));
            Assert.IsNotNull(administratorInDb);
        }


        private void LoadAdministrators(List<Administrator> administrators)
        {
            administrators.ForEach(a => _context.Administrators.Add(a));
            _context.SaveChanges();
        }

        private List<Administrator> CreateAdministrators()
        {
            return new List<Administrator>()
        {
            new()
            {
                Email = "paulaolivera2@gmail.com",
                Address = "de la vega",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "56899",
            },
            new()
            {
                Email = "paulaolivera3@gmail.com",
                Address = "fernandez crespo",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "54s6dfadf",
            }
        };
        }
    }
}
