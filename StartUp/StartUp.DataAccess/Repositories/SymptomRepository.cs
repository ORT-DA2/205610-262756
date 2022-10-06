using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using System;
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

        public override Symptom GetOneByExpression(Expression<Func<Symptom, bool>> expression)
        {
            return _context.Set<Symptom>().FirstOrDefault(expression);
        }

        public override void InsertOne(Symptom elem)
        {
            _context.Entry(elem.SymptomDescription).State = EntityState.Unchanged;
            _context.Set<Symptom>().Add(elem);
        }
    }
}
