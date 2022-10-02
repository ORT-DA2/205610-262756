using StartUp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace StartUp.Domain.SearchCriterias
{
    public class UserSearchCriteria
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public Role? Roles { get; set; }
        public DateTime? RegisterDate { get; set; }
        public Invitation? Invitation { get; set; }
        public Pharmacy Pharmacy { get; set; }
    }
}
