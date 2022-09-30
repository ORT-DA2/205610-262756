using Domain;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.BusinessLogic
{
    public class TokenAccessManager : ITokenAccessManager
    {
        private readonly IRepository<TokenAccess> _tokenRepository;
        private Validator validator = new Validator();

        public TokenAccessManager(IRepository<TokenAccess> tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public List<TokenAccess> GetAllTokenAccess()
        {
            Expression<Func<TokenAccess, bool>> tokenFilter = token => true;

            return _tokenRepository.GetAllExpression(tokenFilter).ToList();
        }

        public TokenAccess GetSpecificTokenAccess(Session session)
        {
            validator.ValidateSessionNotNull(session, "Session empty");

            var tokenSaved = _tokenRepository.GetOneByExpression(t => t.User.Invitation.UserName == session.Username);

            if (tokenSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified token for the {session.Username}");
            }

            return tokenSaved;
        }

        public TokenAccess CreateTokenAccess(User user)
        {
            var tokenAccess = new TokenAccess();
            tokenAccess.Token = Guid.NewGuid();
            tokenAccess.User = user;

            _tokenRepository.InsertOne(tokenAccess);
            _tokenRepository.Save();

            return tokenAccess;
        }
        public TokenAccess UpdateTokenAccess(Session session, TokenAccess updateTokenAccess)
        {
            validator.ValidateSessionNotNull(session, "Session empty");
            validator.ValidateTokenAccess(updateTokenAccess, "Token for update is empty");

            var tokenStored = GetSpecificTokenAccess(session);
            validator.ValidateTokenAccess(tokenStored, "Token not exist in the system");

            tokenStored.User = updateTokenAccess.User;
            tokenStored.Token = updateTokenAccess.Token;

            _tokenRepository.UpdateOne(tokenStored);
            _tokenRepository.Save();

            return tokenStored;
        }
        public void DeleteTokenAccess(Session session)
        {
            validator.ValidateSessionNotNull(session, "Session empty");

            var tokenStored = GetSpecificTokenAccess(session);
            validator.ValidateTokenAccess(tokenStored, "Token not exist");

            _tokenRepository.DeleteOne(tokenStored);
            _tokenRepository.Save();
        }
    }
}
