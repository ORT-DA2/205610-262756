﻿using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;

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
            //var retrievedAdmins = _adminManager.GetAllAdministrator(searchCriteria.ToEntity());
            //return Ok(retrievedAdmins.Select(m => new AdministratorBasicModel(m)));
            return (Ok());
        }

        // Show - Get specific movie (/api/movies/{id})
        [HttpGet("{adminEmail}", Name = "GetAdmin")]
        public IActionResult GetAdministrator(string email)
        {/*
                try
                {
                    var retrievedAdmin = _adminManager.GetSpecificAdministrator(email);
                    return Ok(new AdministratorDetailModel(retrievedAdmin));
                }
                catch (ResourceNotFoundException e)
                {
                    return NotFound(e.Message);
                }*/
            return NotFound();
        }

        /*
        // Create - Create new movie (/api/movies)
        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieModel newMovie)
        {
            try
            {
                var createdMovie = _movieManager.CreateMovie(newMovie.ToEntity());
                var movieModel = new MovieDetailModel(createdMovie);
                return CreatedAtRoute("GetMovie", new { movieId = movieModel.Id }, movieModel);
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update - Update specific movie (/api/movies/{id})
        [HttpPut("{movieId}")]
        public IActionResult Update(int movieId, [FromBody] MovieModel updatedMovie)
        {
            try
            {
                var retrievedMovie = _movieManager.UpdateMovie(movieId, updatedMovie.ToEntity());
                return Ok(new MovieDetailModel(retrievedMovie));
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

        // Delete - Delete specific movie (/api/movies/{id})
        [HttpDelete("{movieId}")]
        public IActionResult Delete(int movieId)
        {
            try
            {
                _movieManager.DeleteMovie(movieId);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }*/
    }
}
