﻿using IBusinessLogic;
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
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Pharmacy> _pharmacyRepository;
        private readonly IRepository<Invitation> _invitationRepository;

        public UserService(IRepository<User> userRepository, IRepository<Role> roleRepository, 
            IRepository<Pharmacy> pharmacyRepository, IRepository<Invitation> invitationRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _pharmacyRepository = pharmacyRepository;
            _invitationRepository = invitationRepository;
        }

        public List<User> GetAllUser(UserSearchCriteria searchCriteria)
        {
            Expression<Func<User, bool>> userFilter = user => true;

            return _userRepository.GetAllByExpression(userFilter).ToList();
        }

        public User GetSpecificUser(int userId)
        {
            Validator validator = new Validator();
            validator.ValidateString(userId.ToString(), "User empty");

            User UserSaved = _userRepository.GetOneByExpression(u => u.Id == userId);

            if(UserSaved == null)
            {
                throw new InputException($"The user {userId} not exist");
            }

            return UserSaved;
        }
        
        public User GetSpecificUserByUserName(string username)
        {
            Validator validator = new Validator();
            validator.ValidateString(username.ToString(), "User empty");

            User UserSaved = _userRepository.GetOneByExpression(u => u.Invitation.UserName == username);

            if (UserSaved == null)
            {
                throw new InputException($"The user {username} not exist");
            }
            
            return UserSaved;
        }

        public User CreateUser(User user)
        {
            user.IsValidUser();
            EmailNotExistInDataBase(user);
            user.Invitation = _invitationRepository.GetOneByExpression(i => i.UserName == user.Invitation.UserName);
            user.VerifyInvitationExist();
            user.VerifyInvitationState();
            user.VerifyInvitationRoles();
            user.VerifyInvitationPharmacy();
            user.VerifyRolesAndPharmacy();
            user.ChangeStatusInvitation();
            user.RegisterDate = DateTime.Now;

            if ((user.Pharmacy != null) && !user.Roles.Permission.Contains("administrator"))
            {
                user.Pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == user.Pharmacy.Name);
            }
            user.Roles = _roleRepository.GetOneByExpression(r=>r.Permission == user.Roles.Permission);
            _userRepository.InsertOne(user);
            _userRepository.Save();

            return user;
        }

        public User UpdateUser(int userId, User updateduser)
        {
            updateduser.IsValidUser();
            
            var userStored = GetSpecificUser(userId);
            if(userStored == null)
            {
                throw new InputException("User not exist");
            }

            updateduser.Invitation = _invitationRepository.GetOneByExpression(i => i.UserName == updateduser.Invitation.UserName);
            updateduser.VerifyInvitationExist();
            updateduser.VerifyInvitationState();
            updateduser.VerifyInvitationRoles();
            updateduser.VerifyInvitationPharmacy();
            updateduser.VerifyRolesAndPharmacy();

            userStored.Email = updateduser.Email;
            userStored.Address = updateduser.Address;
            userStored.RegisterDate = updateduser.RegisterDate;
            userStored.Password = updateduser.Password;
            userStored.Invitation = updateduser.Invitation;
            userStored.Roles = _roleRepository.GetOneByExpression(r => r.Permission == updateduser.Roles.Permission);

            if (updateduser.Pharmacy != null)
            {
                userStored.Pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == updateduser.Pharmacy.Name);
            }
            
            _userRepository.UpdateOne(userStored);
            _userRepository.Save();

            return userStored;
        }

        public void DeleteUser(int userId)
        {
            Validator validator = new Validator();
            validator.ValidateString(userId.ToString(), "UserID empty");

            var userStored = GetSpecificUser(userId);
            if (userStored == null)
            {
                throw new InputException("User not exist");
            }

            _userRepository.DeleteOne(userStored);
            _userRepository.Save();
        }

        private void EmailNotExistInDataBase(User user)
        {
            if (user == null)
            {
                throw new InputException("User empty");
            }
            
            User userSaved = _userRepository.GetOneByExpression(u => u.Email == user.Email);

            if (userSaved != null)
            {
                throw new InputException($"There is already an User with {user.Email} email");
            }
        }

        public void SaveToken(User user, string token)
        {
            var userSalved = _userRepository.GetOneByExpression(u => u.Invitation.UserName == user.Invitation.UserName);
            
            userSalved.Token = token;
            _userRepository.UpdateOne(userSalved);
            _userRepository.Save();
        }

    }
}
