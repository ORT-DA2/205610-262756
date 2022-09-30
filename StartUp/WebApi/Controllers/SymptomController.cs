using Microsoft.AspNetCore.Mvc;
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
        private readonly ISymptomService _symptomService;

        public SymptomController(ISymptomService service)
        {
            _symptomService = service;
        }

        // Index - Get all symptom (/api/symptom)
        [HttpGet]
        public IActionResult GetSymptom([FromQuery] SymptomSearchCriteriaModel searchCriteria)
        {
            var retrievedSymptom = _symptomService.GetAllSymptom(searchCriteria.ToEntity());
            return Ok(retrievedSymptom.Select(s => new SymptomBasicModel(s)));
        }

        // Show - Get specific symptom (/api/symptom/{id})
        [HttpGet("{id}", Name = "GetSymptom")]
        public IActionResult GetSymptom(int id)
        {
            var retrievedSymptom = _symptomService.GetSpecificSymptom(id);
            return Ok(new SymptomDetailModel(retrievedSymptom));
        }

        // Create - Create new symptom (/api/symptom)
        [HttpPost]
        public IActionResult CreateSymptom([FromBody] SymptomModel newSymptom)
        {
            var createdSymptom = _symptomService.CreateSymptom(newSymptom.ToEntity());
            var symptomModel = new SymptomDetailModel(createdSymptom);
            return CreatedAtRoute("GetSymptom", new { id = symptomModel.Id }, symptomModel);
        }

        // Update - Update specific symptom (/api/symptom/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SymptomModel updatedSymptom)
        {
            var retrievedSymptom = _symptomService.UpdateSymptom(id, updatedSymptom.ToEntity());
            return Ok(new SymptomDetailModel(retrievedSymptom));
        }

        // Delete - Delete specific symptom (/api/symptom/{id})
        [HttpDelete("{id}")]
        public IActionResult DeleteSymptom(int id)
        {
            _symptomService.DeleteSymptom(id);
            return Ok();
        }
    }
}
