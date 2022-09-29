﻿using Microsoft.AspNetCore.Mvc;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.Models.Models.In;
using StartUp.Models.Models.Out;
using System.Linq;

namespace StartUp.WebApi.Controllers
{

    [Route("api/invoiceLine")]
    [ApiController]
    public class InvoiceLineController : ControllerBase
    {
        private readonly IInvoiceLineService _invoiceLineService;

        public InvoiceLineController(IInvoiceLineService service)
        {
            _invoiceLineService = service;
        }

        // Index - Get all invoiceLine (/api/invoiceLine)
        [HttpGet]
        public IActionResult GetInvoiceLine([FromQuery] InvoiceLineSearchCriteriaModel searchCriteria)
        {
            var retrievedInvoiceLine = _invoiceLineService.GetAllInvoiceLine(searchCriteria.ToEntity());
            return Ok(retrievedInvoiceLine.Select(i => new InvoiceLineBasicModel(i)));
        }

        // Show - Get specific invoiceLine (/api/invoiceLine/{id})
        [HttpGet("{id}", Name = "GetInvoiceLine")]
        public IActionResult GetInvoiceLine(int id)
        {
            try
            {
                var retrievedInvoiceLine = _invoiceLineService.GetSpecificInvoiceLine(id);
                return Ok(new InvoiceLineDetailModel(retrievedInvoiceLine));
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // Create - Create new invoiceLine (/api/invoiceLine)
        [HttpPost]
        public IActionResult CreateInvoiceLine([FromBody] InvoiceLineModel newInvoiceLine)
        {
            try
            {
                var createdInvoiceLine = _invoiceLineService.CreateInvoiceLine(newInvoiceLine.ToEntity());
                var invoiceLineModel = new InvoiceLineDetailModel(createdInvoiceLine);
                return CreatedAtRoute("GetInvoiceLine", new { id = invoiceLineModel.Id }, invoiceLineModel);
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update - Update specific invoiceLine (/api/invoiceLine/{id})
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] InvoiceLineModel updatedInvoiceLine)
        {
            try
            {
                var retrievedInvoiceLine = _invoiceLineService.UpdateInvoiceLine(id, updatedInvoiceLine.ToEntity());
                return Ok(new InvoiceLineDetailModel(retrievedInvoiceLine));
            }
            catch (InvalidResourceException e)
            {
                return BadRequest(e.Message);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // Delete - Delete specific invoiceLine (/api/invoiceLine/{id})
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _invoiceLineService.DeleteInvoiceLine(id);
                return Ok();
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
