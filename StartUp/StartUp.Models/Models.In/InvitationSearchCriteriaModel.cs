using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Models.Models.In
{
    public class InvitationSearchCriteriaModel
    {
        public string? UserName { get; set; }
        public string? Rol { get; set; }


        public InvitationSearchCriteria ToEntity()
        {
            return new InvitationSearchCriteria()
            {
                UserName = this.UserName,
                Rol = this.Rol,
            };
        }
    }
}
