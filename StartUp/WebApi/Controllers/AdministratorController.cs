using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StartUp.Exceptions;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;

namespace StartUp.WebApi.Controllers
{

    [Route("api/administrator")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _adminService;

        public AdministratorController(IAdministratorService service)
        {
            _adminService = service;
        }

        // Index - Get all administrator (/api/administrator)
        [HttpGet]
        public IActionResult GetAdministrator([FromQuery] AdministratorSearchCriteriaModel searchCriteria)
        {
            var retrievedAdmins = _adminService.GetAllAdministrator(searchCriteria.ToEntity());
            return Ok(retrievedAdmins.Select(a => new AdministratorBasicModel(a)));
        }
        
        // Show - Get specific administrator (/api/administrator/{id})
        [HttpGet("{id}", Name = "GetAdmin")]
        public IActionResult GetAdministrator(int id)
        {
            try
            {
                var retrievedAdmin = _adminService.GetSpecificAdministrator(id);
                return Ok(new AdministratorDetailModel(retrievedAdmin));
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // Create - Create new administrator (/api/administrator)
        [HttpPost]
        public IActionResult CreateAdministrator([FromBody] AdministratorModel newAdministrator)
        {
            try
            {
                var createdAdministrator = _adminService.CreateAdministrator(newAdministrator.ToEntity());
                var adminModel = new AdministratorDetailModel(createdAdministrator);
                return CreatedAtRoute("GetAdmin", new { id = adminModel.Id }, adminModel);
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update - Update specific administrator (/api/administrator/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AdministratorModel updatedAdmin)
        {
            try
            {
                var retrievedAdmin = _adminService.UpdateAdministrator(id, updatedAdmin.ToEntity());
                return Ok(new AdministratorDetailModel(retrievedAdmin));
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

        // Delete - Delete specific administrator (/api/administrator/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _adminService.DeleteAdministrator(id);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
    }
