using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IEmployeeManager
    {
        List<Employee> GetAllEmployee(EmployeeSearchCriteria searchCriteria);
        Employee GetSpecificEmployee(string email);
        Employee CreateEmployee(Employee employee);
        Employee UpdateEmployee(string email, Employee employee);
        Employee DeleteEmployee(string email);
    }
}
