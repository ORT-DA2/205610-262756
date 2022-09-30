using DbUp;
using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using StartUp.WebApi.Filters;
using System.Linq;

namespace StartUp.WebApi.Controllers
{
    [Route("api/request")]
    [ApiController]
    //[Filters(AuthorizationFilter())]
    //SOLO PUEDEN TENER ACCESO LOS DUEÑOS Y EMPLEADOS
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService service)
        {
            _requestService = service;
        }

        // Index - Get all request (/api/request)
        [HttpGet]
        public IActionResult GetRequest([FromQuery] RequestSearchCriteriaModels searchCriteria)
        {
            var retrievedRequest = _requestService.GetAllRequest(searchCriteria.ToEntity());
            return Ok(retrievedRequest.Select(r => new RequestBasicModel(r)));
        }

        // Show - Get specific request (/api/request/{id})
        [HttpGet("{id}", Name = "GetRequest")]
        public IActionResult GetRequest(int id)
        {
            var retrievedRequest = _requestService.GetSpecificRequest(id);
            return Ok(new RequestDetailModel(retrievedRequest));
        }

        // Create - Create new request (/api/request)
        [HttpPost]
        public IActionResult CreateRequest([FromBody] RequestModel newRequest)
        {
            var createdRequest = _requestService.CreateRequest(newRequest.ToEntity());
            var requestModel = new RequestDetailModel(createdRequest);
            return CreatedAtRoute("GetRequest", new { id = requestModel.Id }, requestModel);
        }

        // Update - Update specific request (/api/request/{id})
        [HttpPut("{id}")]
        //SOLO PUEDEN EDITAR LOS DUEÑOS
        public IActionResult Update(int id, [FromBody] RequestModel updatedRequest)
        {
            var retrievedRequest = _requestService.UpdateRequest(id, updatedRequest.ToEntity());
            return Ok(new RequestDetailModel(retrievedRequest));
        }

        // Delete - Delete specific request (/api/request/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _requestService.DeleteRequest(id);
            return Ok();
        }
    }
}
