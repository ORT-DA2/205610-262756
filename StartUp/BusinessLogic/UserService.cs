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
        private Validator validator = new Validator();

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
            validator.ValidateString(userId.ToString(), "User empty");

            User UserSaved = _userRepository.GetOneByExpression(u => u.Id == userId);

            validator.ValidateUserNotNull(UserSaved, $"The user {userId} not exist");

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
            validator.ValidateUserNotNull(updateduser, "User empty");
            validator.ValidateString(userId.ToString(), "UserId empty");

            updateduser.IsValidUser();

            var userStored = GetSpecificUser(userId);
            validator.ValidateUserNotNull(userStored, "User not exist");

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
            validator.ValidateString(userId.ToString(), "UserID empty");
            var userStored = GetSpecificUser(userId);
            validator.ValidateUserNotNull(userStored, "User not exist");

            _userRepository.DeleteOne(userStored);
            _userRepository.Save();
        }

        private void EmailNotExistInDataBase(User user)
        {
            validator.ValidateUserNotNull(user, "User empty");

            User userSaved = _userRepository.GetOneByExpression(u => u.Email == user.Email);

            validator.ValidateUserNull(userSaved, $"There is already an User with {user.Email} email");
        }

        public void SaveToken(User user, string token)
        {
            validator.ValidateUserNull(user, "User empty");
            validator.ValidateString(token, "Token empty");
            
            var userSalved = _userRepository.GetOneByExpression(u => u.Id == user.Id);
            
            validator.ValidateUserNull((userSalved), "User not exist in database");
            
            userSalved.Token = token;
            _userRepository.Save();
        }

    }
}
