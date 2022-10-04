using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StartUp.BusinessLogic
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly ISessionService _sessionService;
        private readonly IRepository<Pharmacy> _pharmacyRepository;

        public RequestService(IRepository<Request> requestRepository, ISessionService sessionService, IRepository<Pharmacy> pharmacyRepository)
        {
            _requestRepository = requestRepository;
            _sessionService = sessionService;
            _pharmacyRepository = pharmacyRepository;
        }

        public List<Request> GetAllRequest(RequestSearchCriteria searchCriteria)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p=>p.Equals(_sessionService.UserLogged.Pharmacy));

            var stateCriteria = searchCriteria.State?.ToString().ToLower() ?? string.Empty;

            List<Request> requests = new List<Request>();

            if (string.IsNullOrEmpty(stateCriteria))
            {
                requests = pharmacy.Requests;
            }
            else
            {
                foreach (Request request in pharmacy.Requests)
                {
                    if (request.State == searchCriteria.State)
                    {
                        requests.Add(request);
                    }
                }
            }

            return requests;
        }

        public Request GetSpecificRequest(int requestId)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));

            var requestSaved = _requestRepository.GetOneByExpression(r => r.Id == requestId);

            if (requestSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified request {requestId}");
            }

            if (pharmacy.Requests.Contains(requestSaved))
            {
                return requestSaved;
            }
            else
            {
                throw new ResourceNotFoundException($"The request {requestId} does not belong to your pharmacy");
            }
        }

        public Request CreateRequest(Request request)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));
            
            request.isValidRequest();

            pharmacy.Requests.Add(request);
            _pharmacyRepository.UpdateOne(pharmacy);
            _pharmacyRepository.Save();
            _requestRepository.InsertOne(request);
            _requestRepository.Save();

            return request;
        }

        public Request UpdateRequest(int requestId, Request updatedRequest)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));
            var request = GetSpecificRequest(requestId);

            if (pharmacy.Requests.Contains(request))
            {
                updatedRequest.isValidRequest();

                request.State = updatedRequest.State;

                if(updatedRequest.State == true)
                {
                    //updateStockInPharmacy
                }

                _requestRepository.UpdateOne(request);
                _requestRepository.Save();

                return request;
            }
            else
            {
                throw new InputException("The request you want to modify does not belong to your pharmacy");
            }

        }

        public void DeleteRequest(int requestId)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Equals(_sessionService.UserLogged.Pharmacy));

            var requestStored = GetSpecificRequest(requestId);

            if (pharmacy.Requests.Contains(requestStored))
            {
                _requestRepository.DeleteOne(requestStored);
                _requestRepository.Save();
            }
            else
            {
                throw new InputException("The request you want to delete does not belong to your pharmacy");
            }
        }

    }
}
