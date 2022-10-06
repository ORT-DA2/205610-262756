﻿using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using System;
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
            return _context.Set<Pharmacy>().Include("Stock").Include("Requests").Include("Sale").FirstOrDefault(expression);
        }

        public virtual void InsertOne(Pharmacy elem)
        {
            _context.Entry(elem.Sales).State = EntityState.Unchanged;
            _context.Entry(elem.Stock).State = EntityState.Unchanged;
            _context.Entry(elem.Requests).State = EntityState.Unchanged;
            _context.Set<Pharmacy>().Add(elem);
        }
    }
}
