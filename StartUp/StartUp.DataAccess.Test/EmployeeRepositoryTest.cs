using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StartUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StartUp.DataAccess.Test
{
    [TestClass]
    public class EmployeeRepositoryTest
    {
        private BaseRepository<Employee> _repository;
        private StartUpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _context = ContextFactory.GetNewContext(ContextType.SQL);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            _repository = new BaseRepository<Employee>(_context);
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllEmployeeReturnsAsExpected()
        {
            Expression<Func<Employee, bool>> expression = e => e.Email.ToLower().Contains("paulaolivera1995@gmail.com");
            var employees = CreateEmployees();
            var eligibleEmployees = employees.Where(expression.Compile()).ToList();
            LoadEmployees(employees);

            var retrievedEmployees = _repository.GetAllByExpression(expression);
            CollectionAssert.AreEquivalent(eligibleEmployees, retrievedEmployees.ToList());
        }

        [TestMethod]
        public void InsertNewEmployee()
        {
            var employees = CreateEmployees();
            LoadEmployees(employees);
            var newEmployee = new Employee()
            {
                Email = "paulaolivera1995@gmail.com",
                Address = "carlos maria ramirez",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "123ss",
            };

            _repository.InsertOne(newEmployee);
            _repository.Save();

            // Voy directo al contexto a buscarla
            var employeeInDb = _context.Employees.FirstOrDefault(e => e.Email.Equals(newEmployee.Email));
            Assert.IsNotNull(employeeInDb);
        }


        private void LoadEmployees(List<Employee> employees)
        {
            employees.ForEach(e => _context.Employees.Add(e));
            _context.SaveChanges();
        }

        private List<Employee> CreateEmployees()
        {
            return new List<Employee>()
        {
            new()
            {
                Email = "paulaolivera2@gmail.com",
                Address = "de la vega",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "56899",
            },
            new()
            {
                Email = "paulaolivera3@gmail.com",
                Address = "fernandez crespo",
                Invitation = new Invitation(),
                RegisterDate = DateTime.Now,
                Password = "54s6dfadf",
            }
        };
        }
    }
}
