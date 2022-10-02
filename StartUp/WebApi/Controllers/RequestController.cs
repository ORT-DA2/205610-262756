﻿using Microsoft.AspNetCore.Mvc;
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
        [OwnerFilter]
        public IActionResult GetRequest([FromQuery] RequestSearchCriteriaModels searchCriteria)
        {
            var retrievedRequest = _requestService.GetAllRequest(searchCriteria.ToEntity());
            return Ok(retrievedRequest.Select(r => new RequestBasicModel(r)));
        }

        [HttpGet("{id}", Name = "GetRequest")]
        [OwnerFilter]
        public IActionResult GetRequest(int id)
        {
            var retrievedRequest = _requestService.GetSpecificRequest(id);
            return Ok(new RequestDetailModel(retrievedRequest));
        }

        [HttpPost]
        [EmployeeFilter]
        public IActionResult CreateRequest([FromBody] RequestModel newRequest)
        {
            var createdRequest = _requestService.CreateRequest(newRequest.ToEntity());
            var requestModel = new RequestDetailModel(createdRequest);
            return CreatedAtRoute("GetRequest", new { id = requestModel.Id }, requestModel);
        }

        [HttpPut("{id}")]
        [OwnerFilter]
        public IActionResult Update(int id, [FromBody] RequestModel updatedRequest)
        {
            var retrievedRequest = _requestService.UpdateRequest(id, updatedRequest.ToEntity());
            return Ok(new RequestDetailModel(retrievedRequest));
        }

        [HttpDelete("{id}")]
        [OwnerFilter]
        public IActionResult Delete(int id)
        {
            _requestService.DeleteRequest(id);
            return Ok();
        }
    }
}
