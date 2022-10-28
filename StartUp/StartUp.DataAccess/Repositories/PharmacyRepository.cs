using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using System;
using System.Collections.Generic;
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
        
        public override IEnumerable<Pharmacy> GetAllByExpression(Expression<Func<Pharmacy, bool>> expression)
        {
            return _context.Set<Pharmacy>().Include("Stock").Include("Requests").Where(expression).ToList();
        }

        public override Pharmacy GetOneByExpression(Expression<Func<Pharmacy, bool>> expression)
        {
            return _context.Set<Pharmacy>().Include("Stock").Include("Requests").FirstOrDefault(expression);
        }

        public override void InsertOne(Pharmacy elem)
        {
            _context.Set<Pharmacy>().Add(elem);
        }
    }
}
