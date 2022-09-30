using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService service)
        {
            _saleService = service;
        }

        // Index - Get all sale (/api/sale)
        [HttpGet]
        public IActionResult GetSale([FromQuery] SaleSearchCriteriaModel searchCriteria)
        {
            var retrievedSale = _saleService.GetAllSale(searchCriteria.ToEntity());
            return Ok(retrievedSale.Select(s => new SaleBasicModel(s)));
        }

        // Show - Get specific sale (/api/sale/{id})
        [HttpGet("{id}", Name = "GetSale")]
        public IActionResult GetSale(int id)
        {
            var retrievedSale = _saleService.GetSpecificSale(id);
            return Ok(new SaleDetailModel(retrievedSale));
        }

        // Create - Create new sale (/api/sale)
        [HttpPost]
        public IActionResult CreateSale([FromBody] SaleModel newSale)
        {
            var createdSale = _saleService.CreateSale(newSale.ToEntity());
            var saleModel = new SaleDetailModel(createdSale);
            return CreatedAtRoute("GetSale", new { id = saleModel.Id }, saleModel);
        }

        // Update - Update specific sale (/api/sale/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SaleModel updatedSale)
        {
            var retrievedSale = _saleService.UpdateSale(id, updatedSale.ToEntity());
            return Ok(new SaleDetailModel(retrievedSale));
        }

        // Delete - Delete specific sale (/api/sale/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _saleService.DeleteSale(id);
            return Ok();
        }
    }
}
