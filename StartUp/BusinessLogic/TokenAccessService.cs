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
    public class TokenAccessService : ITokenAccessService
    {
        private readonly IRepository<TokenAccess> _tokenRepository;

        public TokenAccessService(IRepository<TokenAccess> tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public List<TokenAccess> GetAllTokenAccess()
        {
            Expression<Func<TokenAccess, bool>> tokenFilter = token => true;

            return _tokenRepository.GetAllByExpression(tokenFilter).ToList();
        }

        public TokenAccess GetSpecificTokenAccess(int tokenId)
        {
            Validator validator = new Validator();
            validator.ValidateString(tokenId.ToString(), "Token empty");

            var tokenSaved = _tokenRepository.GetOneByExpression(t => t.Id == tokenId);

            if (tokenSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified token for the {tokenId}");
            }

            return tokenSaved;
        }

        public TokenAccess GetSpecificTokenAccess(Session session)
        {
            if (session == null)
            {
                throw new InputException("Session empty");
            }

            var tokenSaved = _tokenRepository.GetOneByExpression(t => t.User.Invitation.UserName == session.Username);

            if (tokenSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified token for the {session.Username}");
            }

            return tokenSaved;
        }

        public TokenAccess CreateTokenAccess(User user)
        {
            if(user == null)
            {
                throw new InputException("User empty");
            }
            
            var tokenAccess = new TokenAccess();
            tokenAccess.Token = Guid.NewGuid();
            tokenAccess.User = user;

            _tokenRepository.InsertOne(tokenAccess);
            _tokenRepository.Save();

            return tokenAccess;
        }
        public TokenAccess UpdateTokenAccess(Session session, TokenAccess updateTokenAccess)
        {
            if(session == null)
            {
                throw new InputException("Session empty");
            }
            if(updateTokenAccess == null)
            {
                throw new InputException("Token for update is empty");
            }
            

            var tokenStored = GetSpecificTokenAccess(session);
            if (tokenStored == null)
            {
                throw new InputException("Token not exist in the system");
            }

            tokenStored.User = updateTokenAccess.User;
            tokenStored.Token = updateTokenAccess.Token;

            _tokenRepository.UpdateOne(tokenStored);
            _tokenRepository.Save();

            return tokenStored;
        }
        public void DeleteTokenAccess(Session session)
        {
            if (session == null)
            {
                throw new InputException("Session empty");
            }

            var tokenStored = GetSpecificTokenAccess(session);

            if(tokenStored == null)
            {
                throw new InputException("Token not exist");
            }
            

            _tokenRepository.DeleteOne(tokenStored);
            _tokenRepository.Save();
        }
    }
}
