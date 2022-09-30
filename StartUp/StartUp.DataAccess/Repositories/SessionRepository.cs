using Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Repositories
{
    public class SessionRepository : BaseRepository<Session>
    {
        public SessionRepository(StartUpContext context) : base(context)
        {
        }

    }
}
