﻿using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/symptom")]
    [ApiController]
    public class SymptomController : ControllerBase
    {
        private readonly ISymptomManager _symptomManager;

        public SymptomController(ISymptomManager manager)
        {
            _symptomManager = manager;
        }

        // Index - Get all symptom (/api/symptom)
        [HttpGet]
        public IActionResult GetSymptom([FromQuery] SymptomSearchCriteriaModel searchCriteria)
        {
            var retrievedSymptom = _symptomManager.GetAllSymptom(searchCriteria.ToEntity());
            return Ok(retrievedSymptom.Select(s => new SymptomBasicModel(s)));
        }

        // Show - Get specific symptom (/api/symptom/{id})
        [HttpGet("{id}", Name = "GetSymptom")]
        public IActionResult GetSymptom(int id)
        {
            try
            {
                var retrievedSymptom = _symptomManager.GetSpecificSymptom(id);
                return Ok(new SymptomDetailModel(retrievedSymptom));
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // Create - Create new symptom (/api/symptom)
        [HttpPost]
        public IActionResult CreateSymptom([FromBody] SymptomModel newSymptom)
        {
            try
            {
                var createdSymptom = _symptomManager.CreateSymptom(newSymptom.ToEntity());
                var symptomModel = new SymptomDetailModel(createdSymptom);
                return CreatedAtRoute("GetSymptom", new { id = symptomModel.Id }, symptomModel);
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update - Update specific symptom (/api/symptom/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SymptomModel updatedSymptom)
        {
            try
            {
                var retrievedSymptom = _symptomManager.UpdateSymptom(id, updatedSymptom.ToEntity());
                return Ok(new SymptomDetailModel(retrievedSymptom));
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

        // Delete - Delete specific symptom (/api/symptom/{id})
        [HttpDelete("{id}")]
        public IActionResult DeleteSymptom(int id)
        {
            try
            {
                _symptomManager.DeleteSymptom(id);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
