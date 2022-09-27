using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Repositories
{
    internal class PetitionRepository : BaseRepository<Petition>
    {
        public PetitionRepository(StartUpContext context) : base(context)
        {
        }
    }
}
