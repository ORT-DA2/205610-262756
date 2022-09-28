using IBusinessLogic;
using StartUp.Domain;
using StartUp.Exceptions;
using StartUp.Domain.SearchCriterias;
using System.Collections.Generic;
using System.Linq;
using System;
using StartUp.IDataAccess;
using System.Linq.Expressions;

namespace BusinessLogic
{
    public class AdministratorManager : IAdministratorManager
    {
        private readonly IRepository<Administrator> _adminRepository;

        public AdministratorManager(IRepository<Administrator> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public List<Administrator> GetAllAdministrator(AdministratorSearchCriteria searchCriteria)
        {
            var emailCriteria = searchCriteria.Email?.ToLower() ?? string.Empty;
            var passwordCriteria = searchCriteria.Password?.ToLower() ?? string.Empty;
            var addressCriteria = searchCriteria.Address?.ToLower() ?? string.Empty;
            var userNameCriteria = searchCriteria.Invitation.UserName?.ToLower() ?? string.Empty;
            var registerDateCriteria = searchCriteria.RegisterDate?.ToString() ?? string.Empty;

            Expression<Func<Administrator, bool>> adminFilter = admin =>
                admin.Email.ToLower().Contains(emailCriteria) &&
                admin.Password.ToLower().Contains(passwordCriteria) &&
                admin.Address.ToLower().Contains(addressCriteria) &&
                admin.Invitation.UserName.Contains(userNameCriteria) &&
                admin.RegisterDate.ToString().Contains(registerDateCriteria);

            return _adminRepository.GetAllExpression(adminFilter).ToList();
        }

        public Administrator GetSpecificAdministrator(int adminId)
        {
           Administrator administratorSaved = _adminRepository.GetOneByExpression(a => a.Id == adminId);
            
            if (administratorSaved == null)
            {
                throw new ResourceNotFoundException($"The admin {adminId} not exist");
            }
            return administratorSaved;
        }

        public Administrator CreateAdministrator(Administrator admin)
        {
            admin.IsValidAdministrator();
            EmailNotExistInDataBase(admin);
            admin.VerifyInvitationStateIsAvailable();

            _adminRepository.InsertOne(admin);
            _adminRepository.Save();

            admin.ChangeStatusInvitation();

            return admin;
        }

        public Administrator UpdateAdministrator(int adminId, Administrator updatedAdmin)
        {
            updatedAdmin.IsValidAdministrator();

            var adminStored = GetSpecificAdministrator(adminId);

            adminStored.Email = updatedAdmin.Email;
            adminStored.Password = updatedAdmin.Password;
            adminStored.Address = updatedAdmin.Address;
            adminStored.RegisterDate = updatedAdmin.RegisterDate;
            adminStored.Password = updatedAdmin.Password;
            adminStored.Invitation = updatedAdmin.Invitation;

            _adminRepository.UpdateOne(adminStored);
            _adminRepository.Save();

            return adminStored;
        }

        public void DeleteAdministrator(int adminId)
        {
            var adminStored = GetSpecificAdministrator(adminId);

            _adminRepository.DeleteOne(adminStored);
            _adminRepository.Save();
        }

        ///////////
        private void EmailNotExistInDataBase(Administrator admin)
        {
            Administrator administratorSaved = _adminRepository.GetOneByExpression(a => a == admin);

            if (administratorSaved != null)
            {
                throw new ResourceNotFoundException($"There is already an administrator with { admin.Email} email");
            }
        }

    }
}
