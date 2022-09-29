using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.IBusinessLogic
{
    public interface ISessionManager
    {
        bool ValidateToken();
    }
}
