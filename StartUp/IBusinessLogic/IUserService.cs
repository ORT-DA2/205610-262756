using StartUp.Domain;
using System;
using System.Collections.Generic;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using StartUp.Domain.Entities;

namespace IBusinessLogic
{
    public interface IUserService
    {
        List<User> GetAllUser(UserSearchCriteria searchCriteria);
        User GetSpecificUser(int userId);
        User CreateUser(User user);
        User UpdateUser(int userId, User user);
        void DeleteUser(int userId);
    }
}
