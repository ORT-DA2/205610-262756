using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{

    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeManager _employeeManager;

        public EmployeeController(IEmployeeManager manager)
        {
            _employeeManager = manager;
        }

        // Index - Get all employee (/api/employee)
        [HttpGet]
        public IActionResult GetEmployee([FromQuery] EmployeeSearchCriteriaModel searchCriteria)
        {
            var retrievedEmployees = _employeeManager.GetAllEmployee(searchCriteria.ToEntity());
            return Ok(retrievedEmployees.Select(e => new EmployeeBasicModel(e)));
        }

        // Show - Get specific employee (/api/employee/{email})
        [HttpGet("{employeeEmail}", Name = "GetEmployee")]
        public IActionResult GetEmployee(string employeeEmail)
        {
            try
            {
                var retrievedEmployee = _employeeManager.GetSpecificEmployee(employeeEmail);
                return Ok(new EmployeeDetailModel(retrievedEmployee));
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        // Create - Create new employee (/api/employee)
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeModel newEmployee)
        {
            try
            {
                var createdEmployee = _employeeManager.CreateEmployee(newEmployee.ToEntity());
                var employeeModel = new EmployeeDetailModel(createdEmployee);
                return CreatedAtRoute("GetEmployee", new { employeeId = employeeModel.Email }, employeeModel);
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update - Update specific employee (/api/employee/{email})
        [HttpPut("{employeeEmail}")]
        public IActionResult Update(string employeeEmail, [FromBody] EmployeeModel updatedEmployee)
        {
            try
            {
                var retrievedEmployee = _employeeManager.UpdateEmployee(employeeEmail, updatedEmployee.ToEntity());
                return Ok(new EmployeeDetailModel(retrievedEmployee));
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // Delete - Delete specific employee (/api/employee/{email})
        [HttpDelete("{employeeEmail}")]
        public IActionResult DeleteEmployee(string employeeEmail)
        {
            try
            {
                _employeeManager.DeleteEmployee(employeeEmail);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
