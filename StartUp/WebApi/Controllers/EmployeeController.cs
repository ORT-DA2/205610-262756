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
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService service)
        {
            _employeeService = service;
        }

        // Index - Get all employee (/api/employee)
        [HttpGet]
        public IActionResult GetEmployee([FromQuery] EmployeeSearchCriteriaModel searchCriteria)
        {
            var retrievedEmployees = _employeeService.GetAllEmployee(searchCriteria.ToEntity());
            return Ok(retrievedEmployees.Select(e => new EmployeeBasicModel(e)));
        }

        // Show - Get specific employee (/api/employee/{id})
        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult GetEmployee(int id)
        {
            try
            {
                var retrievedEmployee = _employeeService.GetSpecificEmployee(id);
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
                var createdEmployee = _employeeService.CreateEmployee(newEmployee.ToEntity());
                var employeeModel = new EmployeeDetailModel(createdEmployee);
                return CreatedAtRoute("GetEmployee", new { id = employeeModel.Id }, employeeModel);
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update - Update specific employee (/api/employee/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EmployeeModel updatedEmployee)
        {
            try
            {
                var retrievedEmployee = _employeeService.UpdateEmployee(id, updatedEmployee.ToEntity());
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

        // Delete - Delete specific employee (/api/employee/{id})
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
