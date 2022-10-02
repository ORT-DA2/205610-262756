using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/symptom")]
    [ApiController]
    [EmployeeFilter]
    public class SymptomController : ControllerBase
    {
        private readonly ISymptomService _symptomService;

        public SymptomController(ISymptomService service)
        {
            _symptomService = service;
        }

        [HttpGet]
        public IActionResult GetSymptom([FromQuery] SymptomSearchCriteriaModel searchCriteria)
        {
            var retrievedSymptom = _symptomService.GetAllSymptom(searchCriteria.ToEntity());
            return Ok(retrievedSymptom.Select(s => new SymptomBasicModel(s)));
        }

        [HttpGet("{id}", Name = "GetSymptom")]
        public IActionResult GetSymptom(int id)
        {
            var retrievedSymptom = _symptomService.GetSpecificSymptom(id);
            return Ok(new SymptomDetailModel(retrievedSymptom));
        }

        [HttpPost]
        public IActionResult CreateSymptom([FromBody] SymptomModel newSymptom)
        {
            var createdSymptom = _symptomService.CreateSymptom(newSymptom.ToEntity());
            var symptomModel = new SymptomDetailModel(createdSymptom);
            return CreatedAtRoute("GetSymptom", new { id = symptomModel.Id }, symptomModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SymptomModel updatedSymptom)
        {
            var retrievedSymptom = _symptomService.UpdateSymptom(id, updatedSymptom.ToEntity());
            return Ok(new SymptomDetailModel(retrievedSymptom));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSymptom(int id)
        {
            _symptomService.DeleteSymptom(id);
            return Ok();
        }
    }
}
