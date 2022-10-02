using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Models.Models.In;
using System.Collections.Generic;

namespace StartUp.IBusinessLogic
{
    public interface ISessionService
    {
        public User UserLogged { get; set; }
        List<User> GetAllUser();
        User GetSpecificUser(string username);
        void VerifySessionModel(SessionModel sessionModel);
        void VerifySession(Session session);
        User VerifyToken(string token);
        Session CreateOrRetrieveSession(SessionModel session);
        Session GetSpecificSession(string username);
        Session SearchSession(string username);
        bool ValidateToken(string token);
        bool HasPermission(string roles, User user);
        User GetUserToken(string token);
        void Delete(string username);
        bool IsFormatValidOfAuthorizationHeader(string authorizationHeader);
        TokenAccess GetTokenUser();
        void AuthenticateAndSaveUser(string authorizationHeader);
        void Update(Session updateSession);
        void Update(string username);
        public string x { get; set; }
    }
}
