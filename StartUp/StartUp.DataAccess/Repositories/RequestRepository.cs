using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using System;
using System.Collections.Generic;
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
        
        public override IEnumerable<Request> GetAllByExpression(Expression<Func<Request, bool>> expression)
        {
            return _context.Set<Request>().Where(expression).ToList();
        }

        public override Request GetOneByExpression(Expression<Func<Request, bool>> expression)
        {
            var requests = _context.Set<Request>().Include("Petitions").FirstOrDefault(expression);
            return _context.Set<Request>().Include("Petitions").FirstOrDefault(expression);
        }

        public override void InsertOne(Request elem)
        {
            _context.Set<Request>().Add(elem);
        }
    }
}
