using Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.IBusinessLogic
{
    public interface ITokenAccessService
    {
        List<TokenAccess> GetAllTokenAccess();
        TokenAccess GetSpecificTokenAccess(Session session);
        TokenAccess CreateTokenAccess(User user);
        TokenAccess UpdateTokenAccess(Session session, TokenAccess tokenAccess);
        void DeleteTokenAccess(Session session);
    }
}
