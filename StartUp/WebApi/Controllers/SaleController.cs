using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
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

        [HttpGet]
        [AuthorizationFilter("owner")]
        public IActionResult GetSale([FromQuery] SaleSearchCriteriaModel searchCriteria)
        {
            var retrievedSale = _saleService.GetAllSale(searchCriteria.ToEntity());
            return Ok(retrievedSale.Select(s => new SaleBasicModel(s)));
        }

        [HttpGet("{id}", Name = "GetSale")]
        [AuthorizationFilter("owner")]
        public IActionResult GetSale(int id)
        {
            var retrievedSale = _saleService.GetSpecificSale(id);
            return Ok(new SaleDetailModel(retrievedSale));
        }

        [HttpPost]
        public IActionResult CreateSale([FromBody] SaleModel newSale)
        {
            var createdSale = _saleService.CreateSale(newSale.ToEntity());
            var saleModel = new SaleDetailModel(createdSale);
            return CreatedAtRoute("GetSale", new { id = saleModel.Id }, saleModel);
        }

        [HttpPut("{id}")]
        [AuthorizationFilter("owner")]
        public IActionResult Update(int id, [FromBody] SaleModel updatedSale)
        {
            var retrievedSale = _saleService.UpdateSale(id, updatedSale.ToEntity());
            return Ok(new SaleDetailModel(retrievedSale));
        }

        [HttpDelete("{id}")]
        [AuthorizationFilter("owner")]
        public IActionResult Delete(int id)
        {
            _saleService.DeleteSale(id);
            return Ok();
        }
    }
}
