using Domain;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.IDataAccess;
using StartUp.Models.Models.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.IBusinessLogic
{
    public interface ISessionService
    {
        public User logUser { get; set; }
        List<string> GetAllUsername();
        User GetSpecificUser(string username);
        void Update(string username);
        void Delete(string username);
        public Session CreateOrRetrieveSession(SessionModel session);
        bool ValidateToken(string token);
        string GetTypeOfAuthorization(string userName);
        User GetUserToken(string token);
        void VerifySession(Session session);
        void VerifySessionModel(SessionModel sessionModel);
        Session GetSpecificSession(string username);
        bool Permission(User user, string rol);
        void AddUsernameEmployee(List<string> response, List<Employee> listE);
        void AddUsernameOwners(List<string> response, List<Owner> listO);
        void AddUsernameAdministrators(List<string> response, List<Administrator> listA);
    }
}
