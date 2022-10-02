using StartUp.Domain;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class SaleRepository : BaseRepository<Sale>
    {
        private StartUpContext _context;
        public SaleRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }

        public override Sale GetOneByExpression(Expression<Func<Sale, bool>> expression)
        {
            return _context.Set<Sale>().Include("InvoiceLine").FirstOrDefault(expression);
        }
    }
}
