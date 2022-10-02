using StartUp.Domain;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class PharmacyRepository : BaseRepository<Pharmacy>
    {
        private StartUpContext _context;
        public PharmacyRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }

        public override Pharmacy GetOneByExpression(Expression<Func<Pharmacy, bool>> expression)
        {
            return _context.Set<Pharmacy>().Include("Stock").Include("Request").FirstOrDefault(expression);
        }
    }
}
