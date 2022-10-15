using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class SessionRepository : BaseRepository<Session>
    {
        private StartUpContext _context;
        public SessionRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }
        public override IEnumerable<Session> GetAllByExpression(Expression<Func<Session, bool>> expression)
        {
            return _context.Set<Session>().Where(expression).ToList();
        }

        public override Session GetOneByExpression(Expression<Func<Session, bool>> expression)
        {
            return _context.Set<Session>().FirstOrDefault(expression);
        }

        public override void InsertOne(Session elem)
        {
            _context.Set<Session>().Add(elem);
        }
    }
}
