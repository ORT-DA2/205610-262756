using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using StartUp.Models.Models.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.BusinessLogic
{
    public class SessionService : ISessionService
    {
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<TokenAccess> _tokenAccessRepository;
        private Validator validator = new Validator();
        public User UserLogged { get; set; }
        public SessionService(IRepository<Session> sessionRepository, IRepository<User> userRepository,
                                         IRepository<TokenAccess> tokenRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _tokenAccessRepository = tokenRepository;
        }

        public List<Session> GetAllSession(SessionSearchCriteria searchCriteria)
        {
            var usernameCriteria = searchCriteria.Username.ToLower() ?? string.Empty;

            Expression<Func<Session, bool>> sessions = session =>
            session.Username.ToString().ToLower().Contains(usernameCriteria);
            return _sessionRepository.GetAllByExpression(sessions).ToList();
        }

        public User GetSpecificUser(string username)
        {
            return _userRepository.GetOneByExpression(u => u.Invitation.UserName == username);
        }

        public User VerifySession(SessionModel session)
        {
            validator.ValidateString(session.UserName, "Username empty");
            validator.ValidateString(session.Password, "Password empty");

            var userSalved = GetSpecificUser(session.UserName);

            validator.ValidateUserNotNull(userSalved, "Username not exist in sistem");
            validator.ValidateStringEquals(userSalved.Password, session.Password, "Password incorrect");

            return userSalved;
        }

        public Session CreateOrRetrieveSession(SessionModel session)
        {
            Session sessionSalved;
            try
            {
                sessionSalved = GetSpecificSession(session.UserName);
            }
            catch(InputException e)
            {
                return CreateSession(session);
            }
            
            return sessionSalved;
        }

        public Session CreateSession(SessionModel sessionM)
        {
            validator.ValidateString(sessionM.UserName, "Username empty");
            validator.ValidateString(sessionM.Password, "Password empty");

            Session sessionSalved = new Session();
            sessionSalved.Username = sessionM.UserName;
            sessionSalved.Password = sessionM.Password;

            _sessionRepository.InsertOne(sessionSalved);
            _sessionRepository.Save();

            return sessionSalved;
        }

        public Session GetSpecificSession(string username)
        {
            validator.ValidateString(username, "Username empty");

            Session session = _sessionRepository.GetOneByExpression(s => s.Username == username);
            validator.ValidateSessionNotNull(session, "Username not exist");

            return session;
        }

        public void DeleteSession(string username)
        {
            validator.ValidateString(username, "Username empty");

            var session = _sessionRepository.GetOneByExpression(s => s.Username == username);
            validator.ValidateSessionNotNull(session, "Username not exist");

            _sessionRepository.DeleteOne(session);
            _sessionRepository.Save();
        }
        public Session UpdateSession(string username, Session updateSession)
        {
            validator.ValidateString(updateSession.Username, "Username empty");

            var session = GetSpecificSession(username);
            validator.ValidateSessionNotNull(session, "Username not exist");

            _sessionRepository.UpdateOne(session);
            _sessionRepository.Save();

            return session;
        }

        public Session GetSpecificSession(int sessionId)
        {
            var sessionSaved = _sessionRepository.GetOneByExpression(s => s.Id == sessionId);

            if (sessionSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified session {sessionId}");
            }

            return sessionSaved;
        }

        public bool IsFormatValidOfAuthorizationHeader(string authorizationHeader)
        {
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return false;
            }
            return true;
        }

        public TokenAccess GetUserToken()
        {
            var token = _tokenAccessRepository.GetOneByExpression(t => t.User.Invitation.UserName == UserLogged.Invitation.UserName);
            validator.ValidateTokenAccess(token, "Token empty");

            return token;
        }

        public User GetTokenUser(string token)
        {
            validator.ValidateString(token, "Token is empty");

            var userSalved = _userRepository.GetOneByExpression(u => u.Token == token);
            
            validator.ValidateUserNotNull(userSalved, "Token not exist in system");

            return userSalved;
        }

    }
}
