using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Repositories
{
    public class InvoiceLineRepository : BaseRepository<InvoiceLine>
    {
        public InvoiceLineRepository(StartUpContext context) : base(context)
        {
        }
    }
}
