using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain.SearchCriterias
{
    public class InvitationSearchCriteria
    {
        public string? UserName { get; set; }
        public string? Rol { get; set; }
        public int? Code { get; set; }
        public string? State { get; set; }
        public Pharmacy? Pharmacy { get; set; }
    }
}