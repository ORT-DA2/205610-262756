using StartUp.Domain;
using System;
using System.Collections.Generic;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;

namespace IBusinessLogic
{
    public interface IAdministratorService
    {
        List<Administrator> GetAllAdministrator(AdministratorSearchCriteria searchCriteria);
        Administrator GetSpecificAdministrator(int adminId);
        Administrator CreateAdministrator(Administrator admin);
        Administrator UpdateAdministrator(int adminId, Administrator admin);
        void DeleteAdministrator(int adminId);
    }
}
