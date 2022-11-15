using Microsoft.AspNetCore.Mvc;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService service)
        {
            _requestService = service;
        }

        [HttpGet]
        [AuthorizationFilter("owner")]
        public IActionResult GetRequest([FromQuery] RequestSearchCriteriaModel searchCriteria)
        {
            var retrievedRequest = _requestService.GetAllRequest(searchCriteria.ToEntity());
            return Ok(retrievedRequest.Select(r => new RequestBasicModel(r)));
        }

        [HttpGet("{id}", Name = "GetRequest")]
        [AuthorizationFilter("owner")]
        public IActionResult GetRequest(int id)
        {
            var retrievedRequest = _requestService.GetSpecificRequest(id);
            return Ok(new RequestDetailModel(retrievedRequest));
        }

        [HttpPost]
        [AuthorizationFilter("employee")]
        public IActionResult CreateRequest([FromBody] RequestModel newRequest)
        {
            var createdRequest = _requestService.CreateRequest(newRequest.ToEntity());
            var requestModel = new RequestDetailModel(createdRequest);
            return CreatedAtRoute("GetRequest", new { id = requestModel.Id }, requestModel);
        }

        [HttpPut("{id}")]
        [AuthorizationFilter("owner")]
        public IActionResult Update(int id, [FromBody] RequestModel updatedRequest)
        {
            var retrievedRequest = _requestService.UpdateRequest(id, updatedRequest.ToEntity());
            return Ok(new RequestDetailModel(retrievedRequest));
        }

        [HttpDelete("{id}")]
        [AuthorizationFilter("owner")]
        public IActionResult Delete(int id)
        {
            _requestService.DeleteRequest(id);
            return Ok();
        }
    }
}
