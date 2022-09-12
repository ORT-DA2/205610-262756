using StartUp.Domain;
using System;
using System.Collections.Generic;
using StartUp.Domain.SearchCriterias;

namespace IBusinessLogic
{
    public interface IAdministratorManager
    {
        List<Administrator> GetAllAdministrator(AdministratorSearchCriteria searchCriteria);
        Administrator GetSpecificAdministrator(string email);
        Administrator CreateAdministrator(Administrator admin);
    }
}
