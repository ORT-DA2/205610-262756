using StartUp.Domain;
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

        public RequestService(IRepository<Request> requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public List<Request> GetAllRequest(RequestSearchCriteria searchCriteria)
        {
            var petitionsCriteria = searchCriteria.Petitions ?? null;
            var stateCriteria = searchCriteria.State?.ToString().ToLower() ?? string.Empty;

            Expression<Func<Request, bool>> requestFilter = request =>
                request.State.ToString().ToLower().Contains(stateCriteria) &&
                request.Petitions == petitionsCriteria;

            return _requestRepository.GetAllByExpression(requestFilter).ToList();
        }

        public Request GetSpecificRequest(int requestId)
        {
            var requestSaved = _requestRepository.GetOneByExpression(r => r.Id == requestId);

            if (requestSaved is null)
            {
                throw new ResourceNotFoundException($"Could not find specified request {requestId}");
            }
            return requestSaved;
        }

        public Request CreateRequest(Request request)
        {
            request.isValidRequest();

            _requestRepository.InsertOne(request);
            _requestRepository.Save();

            return request;
        }

        public Request UpdateRequest(int requestId, Request updatedRequest)
        {
            updatedRequest.isValidRequest();

            var requestStored = GetSpecificRequest(requestId);

            requestStored.State = updatedRequest.State;

            _requestRepository.UpdateOne(requestStored);
            _requestRepository.Save();

            return requestStored;
        }

        public void DeleteRequest(int requestId)
        {
            var requestStored = GetSpecificRequest(requestId);

            _requestRepository.DeleteOne(requestStored);
            _requestRepository.Save();
        }

    }
}
