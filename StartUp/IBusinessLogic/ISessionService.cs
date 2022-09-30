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
        List<User> GetAllUser();
        User GetSpecificUser(string username);
        void VerifySessionModel(SessionModel sessionModel);
        void VerifySession(Session session);
        User VerifyToken(string token);
        Session CreateOrRetrieveSession(SessionModel session);
        Session GetSpecificSession(string username);
        bool ValidateToken(string token);
        bool Permission(User user, string rol);
        User GetUserToken(string token);
        void Delete(string username);
        void Update(Session updateSession);
        void Update(string username);
    }
}
