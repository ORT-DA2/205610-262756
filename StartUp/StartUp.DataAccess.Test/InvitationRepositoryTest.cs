using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class InvitationRepositoryTest
    {
        private BaseRepository<Invitation> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Invitation>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllInvitationReturnsAsExpected()
        {
            Expression<Func<Invitation, bool>> expression = i => i.Rol.ToLower().Contains("owner");
            var invitations = CreateInvitations();
            var eligibleInvitations = invitations.Where(expression.Compile()).ToList();
            LoadInvitations(invitations);

            var retrievedInvitations = _repository.GetAllByExpression(expression);
            CollectionAssert.AreEquivalent(eligibleInvitations, retrievedInvitations.ToList());
        }

        [TestMethod]
        public void InsertNewInvitation()
        {
            var invitations = CreateInvitations();
            LoadInvitations(invitations);
            var newInvitation = new Invitation()
            {
                Pharmacy = new Pharmacy(),
                Rol = "owner",
                UserName = "paulaO",
            };

            _repository.InsertOne(newInvitation);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var invitationInDb = _context.Invitations.FirstOrDefault(i => i.Rol.Equals(newInvitation.Rol));
            Assert.IsNotNull(invitationInDb);
        }


        private void LoadInvitations(List<Invitation> invitations)
        {
            invitations.ForEach(i => _context.Invitations.Add(i));
            _context.SaveChanges();
        }

        private List<Invitation> CreateInvitations()
        {
            return new List<Invitation>()
        {
            new()
            {
                Pharmacy = new Pharmacy(),
                Rol = "administrator",
                UserName = "paulaOlivera",
            },
            new()
            {
                Pharmacy = new Pharmacy(),
                Rol = "employee",
                UserName = "paulaOB",
            }
        };
        }
    }
}
