﻿using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using System;
using System.Collections.Generic;
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

        public override IEnumerable<InvoiceLine> GetAllByExpression(Expression<Func<InvoiceLine, bool>> expression)
        {
            return _context.Set<InvoiceLine>().Where(expression).ToList();
        }
        
        public override InvoiceLine GetOneByExpression(Expression<Func<InvoiceLine, bool>> expression)
        {
            return _context.Set<InvoiceLine>().Include("Medicine").FirstOrDefault(expression);
        }

        public override void InsertOne(InvoiceLine elem)
        {
            _context.Set<InvoiceLine>().Add(elem);
        }
    }
}
