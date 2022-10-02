﻿using StartUp.Domain;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class InvoiceLineRepository : BaseRepository<InvoiceLine>
    {
        private StartUpContext _context;
        public InvoiceLineRepository(StartUpContext context) : base(context)
        {
            _context = context; 
        }

        public override InvoiceLine GetOneByExpression(Expression<Func<InvoiceLine, bool>> expression)
        {
            return _context.Set<InvoiceLine>().Include("Medicine").FirstOrDefault(expression);
        }
    }
}
