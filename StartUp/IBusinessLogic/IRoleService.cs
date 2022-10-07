using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.IBusinessLogic
{
    public interface IRoleService
    {
        List<Role> GetAllRole(RoleSearchCriteria searchCriteria);
        Role GetSpecificRole(int roleId);
        Role CreateRole(Role role);
        Role UpdateRole(int roleId, Role role);
        void DeleteRole(int roleId);
    }
}
