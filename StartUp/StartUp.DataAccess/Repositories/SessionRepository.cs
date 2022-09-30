using StartUp.Domain.Entities;

namespace StartUp.DataAccess.Repositories
{
    public class SessionRepository : BaseRepository<Session>
    {
        public SessionRepository(StartUpContext context) : base(context)
        {
        }

    }
}
