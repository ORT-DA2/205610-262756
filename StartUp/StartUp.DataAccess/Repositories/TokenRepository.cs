using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
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
        
        public override IEnumerable<TokenAccess> GetAllByExpression(Expression<Func<TokenAccess, bool>> expression)
        {
            return _context.Set<TokenAccess>().Where(expression).ToList();
        }

        public override TokenAccess GetOneByExpression(Expression<Func<TokenAccess, bool>> expression)
        {
            return _context.Set<TokenAccess>().Include(t => t.User).FirstOrDefault(expression);
        }

        public override void InsertOne(TokenAccess elem)
        {
            _context.Set<TokenAccess>().Add(elem);
        }
    }
}
