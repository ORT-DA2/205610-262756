using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.Domain.SearchCriterias
{
    public class AdministratorSearchCriteria
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public DateTime? RegisterDate { get; set; }
        public Invitation? Invitation { get; set; }
    }
}
