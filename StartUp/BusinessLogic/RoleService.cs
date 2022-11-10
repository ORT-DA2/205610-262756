using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.BusinessLogic
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public List<Role> GetAllRole(RoleSearchCriteria searchCriteria)
        {
            var permissionsCriteria = searchCriteria.Permission ?? string.Empty;

            Expression<Func<Role, bool>> roleFilter = role => true;

            return _roleRepository.GetAllByExpression(roleFilter).ToList();
        }

        public Role GetSpecificRole(int roleId)
        {
            var roleSaved = _roleRepository.GetOneByExpression(r => r.Id == roleId);

            if (roleSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified role {roleId}");
            }
            return roleSaved;
        }

        public Role CreateRole(Role role)
        {
            role.IsValidRole();
            
            _roleRepository.InsertOne(role);
            _roleRepository.Save();

            return role;
        }

        public Role UpdateRole(int roleId, Role updatedRole)
        {
            updatedRole.IsValidRole();

            var roleStored = GetSpecificRole(roleId);

            roleStored.Permission = updatedRole.Permission;

            _roleRepository.UpdateOne(roleStored);
            _roleRepository.Save();

            return roleStored;
        }

        public void DeleteRole(int roleId)
        {
            var roleStored = GetSpecificRole(roleId);

            _roleRepository.DeleteOne(roleStored);
            _roleRepository.Save();
        }
    }
}
