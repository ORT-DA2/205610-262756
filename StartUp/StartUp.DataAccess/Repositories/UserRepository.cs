using Microsoft.EntityFrameworkCore;
using StartUp.Domain;
using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {

        public UserRepository(StartUpContext context) : base(context) { }

    }
}
