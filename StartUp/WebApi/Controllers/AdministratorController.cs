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

namespace WebApi.Controllers
{

    [Route("api/administrator")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorManager _adminManager;

        public AdministratorController(IAdministratorManager manager)
        {
            _adminManager = manager;
        }

        // Index - Get all administrator (/api/administrator)
        [HttpGet]
        public IActionResult GetAdministrator([FromQuery] AdministratorSearchCriteriaModel searchCriteria)
        {
            //var retrievedAdmins = _adminManager.GetAllAdministrator(searchCriteria.ToEntity());
            //return Ok(retrievedAdmins.Select(m => new AdministratorBasicModel(m)));
            return(Ok());
        }
        
        // Show - Get specific movie (/api/movies/{id})
        [HttpGet("{adminEmail}", Name = "GetAdmin")]
        public IActionResult GetAdministrator(string email)
        {
            try
            {
                var retrievedAdmin = _adminManager.GetSpecificAdministrator(email);
                return Ok(new AdministratorDetailModel(retrievedAdmin));
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
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
