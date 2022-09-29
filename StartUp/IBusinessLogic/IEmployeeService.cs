using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployee(EmployeeSearchCriteria searchCriteria);
        Employee GetSpecificEmployee(int employeeId);
        Employee CreateEmployee(Employee employee);
        Employee UpdateEmployee(int employeeId, Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
