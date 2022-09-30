using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.Models.Models.Out
{
    public class SessionBasicModel
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public SessionBasicModel(Session session)
        {
            this.Id = session.Id;
            this.Username = session.Username;
        }

        public override bool Equals(object? obj)
        {
            if (obj is SessionBasicModel)
            {
                var otherSale = obj as SessionBasicModel;

                return Username == otherSale.Username;
            }
            else
            {
                return false;
            }
        }
    }
}
