using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>
    {
        public OwnerRepository(StartUpContext context) : base(context)
        {
        }
    }
}
