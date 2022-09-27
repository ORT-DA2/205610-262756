using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeManager(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetAllEmployee(EmployeeSearchCriteria searchCriteria)
        {
            var emailCriteria = searchCriteria.Email?.ToLower() ?? string.Empty;
            var addressCriteria = searchCriteria.Address?.ToLower() ?? string.Empty;
            //   HAY QUE AGREGAR farmacia, invitacion y registerdate`?????????
            var regiterDateCriteria = searchCriteria.RegisterDate?.ToString() ?? string.Empty;
            var pharmacyCriteria = searchCriteria.Pharmacy.Name?.ToString() ?? string.Empty;
            var invitationCriteria = searchCriteria.Invitation.UserName?.ToString() ?? string.Empty;

            Expression<Func<Employee, bool>> employeeFilter = employee =>
                employee.Email.ToLower().Contains(emailCriteria) &&
                employee.Address.ToLower().Contains(addressCriteria) &&
                employee.RegisterDate.ToString().Contains(regiterDateCriteria) &&
                employee.Pharmacy.Name.ToLower().Contains(pharmacyCriteria) &&
                employee.Invitation.UserName.ToLower().Contains(invitationCriteria);

            return _employeeRepository.GetAllExpression(employeeFilter).ToList();
        }

        public Employee GetSpecificEmployee(int employeeId)
        {
            var employeeSaved = _employeeRepository.GetOneByExpression(e => e.Id == employeeId);

            if (employeeSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified employee {employeeId}");
            }

            return employeeSaved;
        }

        public Employee CreateEmployee(Employee employee)
        {
            employee.isValidEmployee();

            _employeeRepository.InsertOne(employee);
            _employeeRepository.Save();

            return employee;
        }

        public Employee UpdateEmployee(int employeeId, Employee updatedEmployee)
        {
            updatedEmployee.isValidEmployee();

            var employeeStored = GetSpecificEmployee(employeeId);

            employeeStored.Pharmacy = updatedEmployee.Pharmacy;
            employeeStored.Email = updatedEmployee.Email;
            employeeStored.Password = updatedEmployee.Password;
            employeeStored.Address = updatedEmployee.Address;
            employeeStored.RegisterDate = updatedEmployee.RegisterDate;
            employeeStored.Password = updatedEmployee.Password;
            employeeStored.Invitation = updatedEmployee.Invitation;

            _employeeRepository.UpdateOne(employeeStored);
            _employeeRepository.Save();

            return employeeStored;
        }

        public void DeleteEmployee(int employeeId)
        {
            var employeeStored = GetSpecificEmployee(employeeId);

            _employeeRepository.DeleteOne(employeeStored);
            _employeeRepository.Save();
        }
    }
}
