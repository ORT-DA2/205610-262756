using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Models.Models.In;
using System.Collections.Generic;

namespace StartUp.IBusinessLogic
{
    public interface ISessionService
    {
        List<Session> GetAllSession(SessionSearchCriteria searchCriteria);
        Session GetSpecificSession(string username);
        Session CreateOrRetrieveSession(SessionModel session);
        Session UpdateSession(string username, Session updateSession);
        void DeleteSession(string username);
        User VerifySession(SessionModel sessionModel);
        TokenAccess GetUserToken();
        User GetTokenUser(string token);
        bool IsFormatValidOfAuthorizationHeader(string authorizationHeader);
    }
}
