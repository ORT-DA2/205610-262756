using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestManager _requestManager;

        public RequestController(IRequestManager manager)
        {
            _requestManager = manager;
        }

        // Index - Get all request (/api/request)
        [HttpGet]
        public IActionResult GetRequest([FromQuery] RequestSearchCriteriaModels searchCriteria)
        {
            var retrievedRequest = _requestManager.GetAllRequest(searchCriteria.ToEntity());
            return Ok(retrievedRequest.Select(r => new RequestBasicModel(r)));
        }

        // Show - Get specific request (/api/request/{id})
        [HttpGet("{id}", Name = "GetRequest")]
        public IActionResult GetRequest(int id)
        {
            var retrievedRequest = _requestManager.GetSpecificRequest(id);
            return Ok(new RequestDetailModel(retrievedRequest));
        }

        // Create - Create new request (/api/request)
        [HttpPost]
        public IActionResult CreateRequest([FromBody] RequestModel newRequest)
        {
            var createdRequest = _requestManager.CreateRequest(newRequest.ToEntity());
            var requestModel = new RequestDetailModel(createdRequest);
            return CreatedAtRoute("GetRequest", new { id = requestModel.Id }, requestModel);
        }

        // Update - Update specific request (/api/request/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RequestModel updatedRequest)
        {
            var retrievedRequest = _requestManager.UpdateRequest(id, updatedRequest.ToEntity());
            return Ok(new RequestDetailModel(retrievedRequest));
        }

        // Delete - Delete specific request (/api/request/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _requestManager.DeleteRequest(id);
            return Ok();
        }
    }
}
