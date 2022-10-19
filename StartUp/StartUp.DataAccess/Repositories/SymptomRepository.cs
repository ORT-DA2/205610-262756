using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class SymptomRepository : BaseRepository<Symptom>
    {
        private StartUpContext _context;
        public SymptomRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }
        
        public override IEnumerable<Symptom> GetAllByExpression(Expression<Func<Symptom, bool>> expression)
        {
            return _context.Set<Symptom>().Where(expression).ToList();
        }

        public override Symptom GetOneByExpression(Expression<Func<Symptom, bool>> expression)
        {
            return _context.Set<Symptom>().FirstOrDefault(expression);
        }

        public override void InsertOne(Symptom elem)
        {
            _context.Set<Symptom>().Add(elem);
        }
    }
}
