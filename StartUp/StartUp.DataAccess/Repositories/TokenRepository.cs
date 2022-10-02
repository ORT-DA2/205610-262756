using StartUp.Domain.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class TokenRepository : BaseRepository<TokenAccess>
    {
        private StartUpContext _context;
        public TokenRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }

        public override TokenAccess GetOneByExpression(Expression<Func<TokenAccess, bool>> expression)
        {
            return _context.Set<TokenAccess>().Include(t=>t.User).Include("User").FirstOrDefault(expression);
        }
    }
}
