using StartUp.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.DataAccess.Repositories
{
    public class PetitionRepository : BaseRepository<Petition>
    {
        private StartUpContext _context;
        public PetitionRepository(StartUpContext context) : base(context)
        {
            _context = context;
        }

        public override Petition GetOneByExpression(Expression<Func<Petition, bool>> expression)
        {
            return _context.Set<Petition>().FirstOrDefault(expression);
        }

        public override void InsertOne(Petition elem)
        {
            _context.Set<Petition>().Add(elem);
        }
    }
}
