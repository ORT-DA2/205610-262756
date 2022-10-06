using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/petition")]
    [ApiController]
    //[AuthorizationFilter("employee")]
    public class PetitionController : ControllerBase
    {
        private readonly IPetitionService _petitionService;

        public PetitionController(IPetitionService service)
        {
            _petitionService = service;
        }

        [HttpGet]
        public IActionResult GetPetition([FromQuery] PetitionSearchCriteriaModel searchCriteria)
        {
            var retrievedPetition = _petitionService.GetAllPetition(searchCriteria.ToEntity());
            return Ok(retrievedPetition.Select(p => new PetitionBasicModel(p)));
        }

        [HttpGet("{id}", Name = "GetPetition")]
        public IActionResult GetPetition(int id)
        {
            var retrievedPetition = _petitionService.GetSpecificPetition(id);
            return Ok(new PetitionDetailModel(retrievedPetition));
        }

        [HttpPost]
        public IActionResult CreatePetition([FromBody] PetitionModel newPetition)
        {
            var createdPetition = _petitionService.CreatePetition(newPetition.ToEntity());
            var petitionModel = new PetitionDetailModel(createdPetition);
            return CreatedAtRoute("GetPetition", new { id = petitionModel.Id }, petitionModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PetitionModel updatedPetition)
        {
            var retrievedPetition = _petitionService.UpdatePetition(id, updatedPetition.ToEntity());
            return Ok(new PetitionDetailModel(retrievedPetition));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _petitionService.DeletePetition(id);
            return Ok();
        }
    }
}
