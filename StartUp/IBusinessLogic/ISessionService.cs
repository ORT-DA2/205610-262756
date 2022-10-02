using StartUp.Domain.Entities;
using StartUp.Models.Models.In;
using System.Collections.Generic;

namespace StartUp.IBusinessLogic
{
    public interface ISessionService
    {
        User UserLogged { get; set; }
        User GetSpecificUser(string username);
        public Session CreateOrRetrieveSession(SessionModel session);
        bool ValidateToken(string token);
        User GetUserToken(string token);
        void VerifySession(Session session);
        void VerifySessionModel(SessionModel sessionModel);
        Session GetSpecificSession(string username);
        bool IsFormatValidOfAuthorizationHeader(string authorizationHeader);
        TokenAccess GetTokenUser();
        void AuthenticateAndSaveUser(string authorizationHeader);
    }
}
