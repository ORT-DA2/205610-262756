using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StartUp.DataAccess.Contexts;
using StartUp.DataAccess.Repositories;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class InvitationRepositoryTest
    {
        private BaseRepository<Invitation> _repository;
        private InvitationRepository _invitationRepository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQLite);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Invitation>(_context);
            _invitationRepository = new InvitationRepository(_context);
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
            
            var invitationInDb = _context.Invitations.FirstOrDefault(i => i.Rol.Equals(newInvitation.Rol));
            Assert.IsNotNull(invitationInDb);
        }
        
        [TestMethod]
        public void GetAllInvitationNotExistTest()
        {
            var invitations = CreateInvitations();
            LoadInvitations(invitations);

            Invitation invitation = _invitationRepository.GetOneByExpression(i=>i.Pharmacy.Name.Contains("PharmacyNotExist"));
            
            Assert.IsNull(invitation);
        }
        
        [TestMethod]
        public void InsertInvitationTest()
        {
            var invitations = CreateInvitations();
            LoadInvitations(invitations);

            Invitation inv = CreateInvitation("harol");
            _invitationRepository.InsertOne(inv);
            _invitationRepository.Save();

            var retrivedInv = _invitationRepository.GetOneByExpression(i => i.UserName == "harol");
            
            Assert.IsNotNull(retrivedInv);
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
        
        private Invitation CreateInvitation(string user)
        {
            List<Medicine> listMedicines = new List<Medicine>();
            List<Request> listRequests = new List<Request>();

            Pharmacy pharmacy = new Pharmacy
            {
                Name = "Una farmacia", 
                Address = "18 de julio",
                Stock = listMedicines,
                Requests = listRequests
            };
            
            return new Invitation()
            {
                Id = 1,
                Rol = "Admin",
                UserName = user,
                Code = 100000,
                State = "Available",
                Pharmacy = pharmacy,
            };
        }
    }
}
