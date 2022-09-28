using Microsoft.AspNetCore.Mvc;
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
        private readonly IPetitionManager _petitionManager;

        public PetitionController(IPetitionManager manager)
        {
            _petitionManager = manager;
        }

        // Index - Get all petition (/api/petition)
        [HttpGet]
        public IActionResult GetPetition([FromQuery] PetitionSearchCriteriaModel searchCriteria)
        {
            var retrievedPetition = _petitionManager.GetAllPetition(searchCriteria.ToEntity());
            return Ok(retrievedPetition.Select(p => new PetitionBasicModel(p)));
        }

        // Show - Get specific petition (/api/petition/{id})
        [HttpGet("{id}", Name = "GetPetition")]
        public IActionResult GetPetition(int id)
        {
            var retrievedPetition = _petitionManager.GetSpecificPetition(id);
            return Ok(new PetitionDetailModel(retrievedPetition));
        }

        // Create - Create new petition (/api/petition)
        [HttpPost]
        public IActionResult CreatePetition([FromBody] PetitionModel newPetition)
        {
            var createdPetition = _petitionManager.CreatePetition(newPetition.ToEntity());
            var petitionModel = new PetitionDetailModel(createdPetition);
            return CreatedAtRoute("GetPetition", new { id = petitionModel.Id }, petitionModel);
        }

        // Update - Update specific petition (/api/petition/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PetitionModel updatedPetition)
        {
            var retrievedPetition = _petitionManager.UpdatePetition(id, updatedPetition.ToEntity());
            return Ok(new PetitionDetailModel(retrievedPetition));
        }

        // Delete - Delete specific petition (/api/petition/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _petitionManager.DeletePetition(id);
            return Ok();
        }
    }
}
