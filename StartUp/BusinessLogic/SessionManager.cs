using Domain;
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
    public class SessionManager : ISessionManager
    {
        private readonly IRepository<Session> _sessionRepository;
        private readonly IRepository<Administrator> _adminRepository;
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<TokenAccess> _tokenAccessRepository;
        private Validator validator = new Validator();
        public User logUser { get; set; }
        public SessionManager(IRepository<Session> sessionRepository, IRepository<Administrator> adminRepository,
            IRepository<Owner> ownerRepository, IRepository<Employee> employeeRepository,
            IRepository<TokenAccess> tokenRepository)
        {
            _sessionRepository = sessionRepository;
            _adminRepository = adminRepository;
            _ownerRepository = ownerRepository;
            _employeeRepository = employeeRepository;
            _tokenAccessRepository = tokenRepository;
        }

        public List<string> GetAllUsername()
        {
            Expression<Func<Administrator, bool>> admins = admin => true;
            Expression<Func<Owner, bool>> owners = owner => true;
            Expression<Func<Employee, bool>> employees = employee => true;

            List<Administrator> listA = _adminRepository.GetAllExpression(admins).ToList();
            List<Owner> listO = _ownerRepository.GetAllExpression(owners).ToList();
            List<Employee> listE = _employeeRepository.GetAllExpression(employees).ToList();

            List<string> response = new List<string>();

            AddUsernameAdministrators(response, listA);
            AddUsernameOwners(response, listO);
            AddUsernameEmployee(response, listE);

            return response;
        }

        public User GetSpecificUser(string username)
        {
            Administrator admin = _adminRepository.GetOneByExpression(a => a.Invitation.UserName == username);
            if (admin != null)
            {
                return admin;
            }

            Owner owner = _ownerRepository.GetOneByExpression(o => o.Invitation.UserName == username);
            if (owner != null)
            {
                return owner;
            }

            return _employeeRepository.GetOneByExpression(e => e.Invitation.UserName == username);
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
            //'administrator,owner,employee,anonymous'
            string[] roles = rol.Split(",");
            bool permission = false;
            foreach (string role in roles)
            {
                permission = permission || user.GetType() == rol;
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

        public string GetTypeOfAuthorization(string username)
        {
            validator.ValidateString(username, "Username empty");

            User user = GetSpecificUser(username);
            validator.ValidateUserNotNull(user, "Username not exist");

            return user.GetType();
        }


        /////////////////////////////////

        public void Delete(string username)
        {
            validator.ValidateString(username, "Username empty");

            var session = _sessionRepository.GetOneByExpression(s => s.Username == username);
            validator.ValidateSessionNotNull(session, "Username not exist");

            _sessionRepository.DeleteOne(session);
            _sessionRepository.Save();
        }

        public void AddUsernameEmployee(List<string> response, List<Employee> listE)
        {
            foreach (Employee employee in listE)
            {
                response.Add(employee.Invitation.UserName);
            }
        }

        public void AddUsernameOwners(List<string> response, List<Owner> listO)
        {
            foreach (Owner owner in listO)
            {
                response.Add(owner.Invitation.UserName);
            }
        }

        public void AddUsernameAdministrators(List<string> response, List<Administrator> listA)
        {
            foreach (Administrator admin in listA)
            {
                response.Add(admin.Invitation.UserName);
            }
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
