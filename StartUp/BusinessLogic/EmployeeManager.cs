using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class EmployeeManager : IEmployeeManager
    {
        private static List<Employee> _employees = new List<Employee>()
    {
        
    };

        public List<Employee> GetAllEmployee(EmployeeSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Employee GetSpecificEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Employee CreateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee UpdateEmployee(int id, Employee updatedAdministrator)
        {
            /*updatedMovie.ValidOrFail();
            var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

            if (movieSaved == null)
                throw new ResourceNotFoundException($"Could not find specified movie {id}");

            // Workaround - como no puedo editar el elemento directamente en List, lo elimino y lo vuelvo a insertar actualizado
            var newMovie = new Movie()
            {
                Id = movieSaved.Id,
                Description = updatedMovie.Description,
                Title = updatedMovie.Title
            };
            _movies.Remove(movieSaved);
            _movies.Add(newMovie);*/
            var employee = new Employee();

            return employee;
        }

        public void DeleteEmployee(int id)
        {
            //var movieSaved = _movies.FirstOrDefault(m => m.Id == id);

            //if (movieSaved == null)
            throw new ResourceNotFoundException($"Could not find specified movie {id}");

            //_movies.Remove(movieSaved);
        }

        public Employee GetSpecificEmployee(string email)
        {
            throw new NotImplementedException();
        }

        public Employee UpdateEmployee(string email, Employee employee)
        {
            throw new NotImplementedException();
        }

        public Employee DeleteEmployee(string email)
        {
            throw new NotImplementedException();
        }
    }
}
