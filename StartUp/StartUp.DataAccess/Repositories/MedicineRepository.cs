using StartUp.Domain;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class MedicineRepository : BaseRepository<Medicine>
    {
        private StartUpContext _context;
        public MedicineRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }

        public override Medicine GetOneByExpression(Expression<Func<Medicine, bool>> expression)
        {
            return _context.Set<Medicine>().Include("Symptoms").FirstOrDefault(expression);
        }
    }
}
