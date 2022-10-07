using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Models.Models.In;
using System.Collections.Generic;

namespace StartUp.IBusinessLogic
{
    public interface ISessionService
    {
        User UserLogged { get; set; }
        List<Session> GetAllSession(SessionSearchCriteria searchCriteria);
        User GetSpecificUser(string username);
        User VerifySession(SessionModel session);
        Session CreateOrRetrieveSession(SessionModel session);
        Session CreateSession(SessionModel sessionM);
        Session GetSpecificSession(string username);
        void DeleteSession(string username);
        Session UpdateSession(string username, Session updateSession);
        Session GetSpecificSession(int sessionId);
        bool IsFormatValidOfAuthorizationHeader(string authorizationHeader);
        TokenAccess GetUserToken();
        User GetTokenUser(string token);
        void SaveUserSession();
    }
}
