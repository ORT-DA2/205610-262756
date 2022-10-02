using StartUp.Domain;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class RequestRepository : BaseRepository<Request>
    {
        private StartUpContext _context;
        public RequestRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }

        public override Request GetOneByExpression(Expression<Func<Request, bool>> expression)
        {
            return _context.Set<Request>().Include("Petitions").FirstOrDefault(expression);
        }
    }
}
