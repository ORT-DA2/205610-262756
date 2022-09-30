using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Repositories
{
    public class InvitationRepository : BaseRepository<Invitation>
    {
        public InvitationRepository(StartUpContext context) : base(context)
        {
        }
    }
}
