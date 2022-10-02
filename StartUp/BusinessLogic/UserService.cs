using IBusinessLogic;
using StartUp.Domain;
using StartUp.Exceptions;
using StartUp.Domain.SearchCriterias;
using System.Collections.Generic;
using System.Linq;
using System;
using StartUp.IDataAccess;
using System.Linq.Expressions;
using StartUp.Domain.Entities;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUser(UserSearchCriteria searchCriteria)
        {
            var emailCriteria = searchCriteria.Email?.ToLower() ?? string.Empty;
            var passwordCriteria = searchCriteria.Password?.ToLower() ?? string.Empty;
            var addressCriteria = searchCriteria.Address?.ToLower() ?? string.Empty;
            var invitationCriteria = searchCriteria.Invitation ?? null;
            var registerDateCriteria = searchCriteria.RegisterDate?.ToString() ?? string.Empty;
            var pharmacyCriteria = searchCriteria.Pharmacy ?? null;
            var rolCriteria = searchCriteria.Roles ?? null;

            Expression<Func<User, bool>> userFilter = user =>
                user.Email.ToLower().Contains(emailCriteria) &&
                user.Password.ToLower().Contains(passwordCriteria) &&
                user.Address.ToLower().Contains(addressCriteria) &&
                user.Invitation == invitationCriteria &&
                user.RegisterDate.ToString().Contains(registerDateCriteria) &&
                user.Pharmacy == pharmacyCriteria && user.Roles == rolCriteria;

            return _userRepository.GetAllByExpression(userFilter).ToList();
        }

        public User GetSpecificUser(int userId)
        {
           User UserSaved = _userRepository.GetOneByExpression(u => u.Id == userId);
            
            if (UserSaved == null)
            {
                throw new ResourceNotFoundException($"The user {userId} not exist");
            }
            return UserSaved;
        }

        public User CreateUser(User user)
        {
            user.IsValidUser();
            EmailNotExistInDataBase(user);
            user.VerifyInvitationStateIsAvailable();
            user.ChangeStatusInvitation();

            _userRepository.InsertOne(user);
            _userRepository.Save();

            return user;
        }

        public User UpdateUser(int userId, User updateduser)
        {
            updateduser.IsValidUser();

            var userStored = GetSpecificUser(userId);

            userStored.Email = updateduser.Email;
            userStored.Password = updateduser.Password;
            userStored.Address = updateduser.Address;
            userStored.RegisterDate = updateduser.RegisterDate;
            userStored.Password = updateduser.Password;
            userStored.Invitation = updateduser.Invitation;
            userStored.Pharmacy = updateduser.Pharmacy;
            userStored.Roles = updateduser.Roles;

            _userRepository.UpdateOne(userStored);
            _userRepository.Save();

            return userStored;
        }

        public void DeleteUser(int userId)
        {
            var userStored = GetSpecificUser(userId);

            _userRepository.DeleteOne(userStored);
            _userRepository.Save();
        }

        private void EmailNotExistInDataBase(User user)
        {
            User UserSaved = _userRepository.GetOneByExpression(u => u.Email == user.Email);

            if (UserSaved != null)
            {
                throw new ResourceNotFoundException($"There is already an User with { user.Email} email");
            }
        }

    }
}
