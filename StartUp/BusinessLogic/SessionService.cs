using Microsoft.Extensions.Primitives;
using StartUp.Domain;
using StartUp.Domain.Entities;
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
        public User logUser { get; set; }
        public SessionService(IRepository<Session> sessionRepository, IRepository<User> userRepository,
                                         IRepository<TokenAccess> tokenRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _tokenAccessRepository = tokenRepository;
        }

        public List<User> GetAllUser()
        {
            Expression<Func<User, bool>> users = user => true;
            return _userRepository.GetAllByExpression(users).ToList();
        }

        public User GetSpecificUser(string username)
        {
            return _userRepository.GetOneByExpression(u => u.Invitation.UserName == username);
        }

        public void VerifySessionModel(SessionModel sessionModel)
        { 

            validator.ValidateString(sessionModel.UserName, "Username empty");
            validator.ValidateString(sessionModel.Password, "Password empty");

            var userSalved = GetSpecificUser(sessionModel.UserName);

            validator.ValidateUserNotNull(userSalved, "Username not exist in sistem");
            validator.ValidateStringEquals(userSalved.Password, sessionModel.Password, "Password incorrect");
        }

        public void VerifySession(Session session)
        {

            validator.ValidateString(session.Username, "Username empty");
            validator.ValidateString(session.Password, "Password empty");

            var userSalved = GetSpecificUser(session.Username);

            validator.ValidateUserNotNull(userSalved, "Username not exist in sistem");
            validator.ValidateStringEquals(userSalved.Password, session.Password, "Password incorrect");
        }

        public User VerifyToken(string token)
        {
            validator.ValidateString(token, "Token is empty");

            var tokenSalved = _tokenAccessRepository.GetOneByExpression(t => t.Token.ToString() == token);
            validator.ValidateTokenAccess(tokenSalved, "Token not exist in system");

            return tokenSalved.User;
        }

        public Session CreateOrRetrieveSession(SessionModel session)
        {
            var sessionSalved = GetSpecificSession(session.UserName);

            if (sessionSalved == null)
            {
                sessionSalved = new Session();
                sessionSalved.Username = session.UserName;
                sessionSalved.Password = session.Password;
                
                _sessionRepository.InsertOne(sessionSalved);
                _sessionRepository.Save();

                return sessionSalved;
            }
            return sessionSalved;
        }

        public Session GetSpecificSession(string username)
        {
            validator.ValidateString(username, "Username empty");

            Session session = _sessionRepository.GetOneByExpression(s => s.Username == username);
            validator.ValidateSessionNotNull(session, "Username not exist");
            
            return session;
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            else
            {
                var tokenSalved = _tokenAccessRepository.GetOneByExpression(t => t.Token.ToString() == token.ToString());

                if (tokenSalved == null)
                {
                    return false;
                }
                return true;
            }
        }

        public bool Permission(User user, string rol)
        {
            //'User,owner,employee,anonymous'
            string[] roles = rol.Split(",");
            bool permission = false;
            foreach (string role in roles)
            {
                permission = permission || user.Rol.ToString().ToLower() == rol;
            }
            return permission;
        }

        public User GetUserToken(string token)
        {
            validator.ValidateString(token, "Token is empty");

            var tokenSalved = _tokenAccessRepository.GetOneByExpression(t => t.Token.ToString() == token.ToString());
            validator.ValidateTokenAccess(tokenSalved, "Token not exist in system");

            return tokenSalved.User;
        }

        public void Delete(string username)
        {
            validator.ValidateString(username, "Username empty");

            var session = _sessionRepository.GetOneByExpression(s => s.Username == username);
            validator.ValidateSessionNotNull(session, "Username not exist");

            _sessionRepository.DeleteOne(session);
            _sessionRepository.Save();
        }
        public void Update(Session updateSession)
        {
            validator.ValidateString(updateSession.Username, "Username empty");

            var session = _sessionRepository.GetOneByExpression(s => s.Username == updateSession.Username);
            validator.ValidateSessionNotNull(session, "Username not exist");

            _sessionRepository.UpdateOne(session);
            _sessionRepository.Save();
        }

        public void Update(string username)
        {
            throw new NotImplementedException();
        }

    }
}
