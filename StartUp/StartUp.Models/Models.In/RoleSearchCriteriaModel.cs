using StartUp.Domain.SearchCriterias;

namespace StartUp.Models.Models.In
{
    public class RoleSearchCriteriaModel
    {
        public string? Permission { get; set; }

        public RoleSearchCriteria ToEntity()
        {
            return new RoleSearchCriteria()
            {
                Permission = Permission,
            };
        }
    }
}
