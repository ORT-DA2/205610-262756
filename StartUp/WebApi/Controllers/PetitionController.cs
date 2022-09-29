﻿using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/petition")]
    [ApiController]
    public class PetitionController : ControllerBase
    {
        private readonly IPetitionService _petitionService;

        public PetitionController(IPetitionService service)
        {
            _petitionService = service;
        }

        // Index - Get all petition (/api/petition)
        [HttpGet]
        public IActionResult GetPetition([FromQuery] PetitionSearchCriteriaModel searchCriteria)
        {
            var retrievedPetition = _petitionService.GetAllPetition(searchCriteria.ToEntity());
            return Ok(retrievedPetition.Select(p => new PetitionBasicModel(p)));
        }

        // Show - Get specific petition (/api/petition/{id})
        [HttpGet("{id}", Name = "GetPetition")]
        public IActionResult GetPetition(int id)
        {
            try
            {
                var retrievedPetition = _petitionService.GetSpecificPetition(id);
                return Ok(new PetitionDetailModel(retrievedPetition));
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // Create - Create new petition (/api/petition)
        [HttpPost]
        public IActionResult CreatePetition([FromBody] PetitionModel newPetition)
        {
            try
            {
                var createdPetition = _petitionService.CreatePetition(newPetition.ToEntity());
                var petitionModel = new PetitionDetailModel(createdPetition);
                return CreatedAtRoute("GetPetition", new { id = petitionModel.Id }, petitionModel);
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update - Update specific petition (/api/petition/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PetitionModel updatedPetition)
        {
            try
            {
                var retrievedPetition = _petitionService.UpdatePetition(id, updatedPetition.ToEntity());
                return Ok(new PetitionDetailModel(retrievedPetition));
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

        // Delete - Delete specific petition (/api/petition/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _petitionService.DeletePetition(id);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }



    }
}
