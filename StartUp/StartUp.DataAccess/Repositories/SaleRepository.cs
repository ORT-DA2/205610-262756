using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using System;
using System.Collections.Generic;
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
        
        public override IEnumerable<Sale> GetAllByExpression(Expression<Func<Sale, bool>> expression)
        {
            return _context.Set<Sale>().Where(expression).ToList();
        }

        public override Sale GetOneByExpression(Expression<Func<Sale, bool>> expression)
        {
            return _context.Set<Sale>().Include("InvoiceLines").FirstOrDefault(expression);
        }

        public override void InsertOne(Sale elem)
        {
            _context.Set<Sale>().Add(elem);
        }
    }
}
