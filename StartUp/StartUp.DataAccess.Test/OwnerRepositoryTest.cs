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
    public class OwnerRepositoryTest
    {
        private BaseRepository<Owner> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Owner>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllOwnerReturnsAsExpected()
        {
            Expression<Func<Owner, bool>> expression = o => o.Email.ToLower().Contains("paulaolivera1995@gmail.com");
            var owners = CreateOwners();
            var eligibleOwners = owners.Where(expression.Compile()).ToList();
            LoadOwners(owners);

            var retrievedOwners = _repository.GetAllExpression(expression);
            CollectionAssert.AreEquivalent(eligibleOwners, retrievedOwners.ToList());
        }

        [TestMethod]
        public void InsertNewOwner()
        {
            var owners = CreateOwners();
            LoadOwners(owners);
            var newOwner = new Owner()
            {
                Email = "paulaolivera1995@gmail.com",
                Address = "carlos maria ramirez",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "123ss",
            };

            _repository.InsertOne(newOwner);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var ownerInDb = _context.Owners.FirstOrDefault(o => o.Email.Equals(newOwner.Email));
            Assert.IsNotNull(ownerInDb);
        }


        private void LoadOwners(List<Owner> owners)
        {
            owners.ForEach(o => _context.Owners.Add(o));
            _context.SaveChanges();
        }

        private List<Owner> CreateOwners()
        {
            return new List<Owner>()
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
