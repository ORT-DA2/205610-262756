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
        [AuthorizationFilter("owner,employee")]
        public IActionResult GetSale()
        {
            var retrievedSale = _saleService.GetAllSale();
            return Ok(retrievedSale.Select(s => new SaleDetailModel(s)));
        }

        [HttpGet("{id}", Name = "GetSale")]
        public IActionResult GetSale(int id)
        {
            var retrievedSale = _saleService.GetSpecificSale(id);
            retrievedSale = _saleService.FilterByPharmacy(retrievedSale);
            return Ok(new SaleDetailModel(retrievedSale));
        }

        [HttpPost]
        public IActionResult CreateSale([FromBody] SaleModel newSale)
        {
            var createdSale = _saleService.CreateSale(newSale.ToEntity());
            var saleModel = new SaleDetailModel(createdSale);
            return CreatedAtRoute("GetSale", new { id = saleModel.Id, code = saleModel.Code }, saleModel);
        }

        [HttpPut("{code}")]
        [AuthorizationFilter("employee")]
        public IActionResult UpdateSale(int code, [FromBody] SaleModel updateSale)
        {
            var retrievedSale = _saleService.UpdateSale(code, updateSale.ToEntity());
            return Ok(new SaleDetailModel(retrievedSale));
        }

        
        [HttpDelete("{code}")]
        public IActionResult GetSaleForCode(string code)
        {
            var retrievedSale = _saleService.GetSpecificSaleForCode(code);
            return Ok(new SaleDetailModel(retrievedSale));
        }
    }
}
