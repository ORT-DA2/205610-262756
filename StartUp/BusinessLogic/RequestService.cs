using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.Domain.SearchCriterias;
using StartUp.Exceptions;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;
using System.Collections.Generic;


namespace StartUp.BusinessLogic
{
    public class RequestService : IRequestService
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly ISessionService _sessionService;
        private readonly IRepository<Pharmacy> _pharmacyRepository;
    

        public RequestService(IRepository<Request> requestRepository, ISessionService sessionService,
            IRepository<Pharmacy> pharmacyRepository)
        {
            _requestRepository = requestRepository;
            _sessionService = sessionService;
            _pharmacyRepository = pharmacyRepository;  
            
        }

        public List<Request> GetAllRequest(RequestSearchCriteria searchCriteria)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == _sessionService.UserLogged.Pharmacy.Name);

            var stateCriteria = searchCriteria.State?.ToString().ToLower() ?? string.Empty;

            List<Request> requests = new List<Request>();

            if (string.IsNullOrEmpty(stateCriteria))
            {
                requests = pharmacy.Requests;
            }
            else
            {
                requests = FilteredRequest(pharmacy, searchCriteria.State, requests);
            }
            return requests;
        }

        public Request GetSpecificRequest(int requestId)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            var requestSaved = _requestRepository.GetOneByExpression(r => r.Id == requestId);
            if(requestSaved == null)
            {
                throw new InputException($"Could not find specified request { requestId }");
            }
            
            if (!pharmacy.Requests.Contains(requestSaved))
            {
                throw new ResourceNotFoundException($"The request {requestId} does not belong to your pharmacy");
            }
            return requestSaved;
        }

        public Request CreateRequest(Request request)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == _sessionService.UserLogged.Pharmacy.Name);

            request.isValidRequest();
            request.State = "Pending";
            
            pharmacy.Requests.Add(request);
            ModifiedRecords(pharmacy, request, false);

            return request;
        }

        public Request UpdateRequest(int requestId, Request updatedRequest)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Name == _sessionService.UserLogged.Pharmacy.Name);
            var request = GetSpecificRequest(requestId);

            updatedRequest.isValidRequest();

            if (request.State.ToLower() == "pending")
            {
                request.State = updatedRequest.State;

                if (request.State.ToLower() == "approved")
                {
                    pharmacy = UpdateStockInPharmacy(pharmacy, request);
                    _pharmacyRepository.UpdateOne(pharmacy);
                    _pharmacyRepository.Save();
                }

                _requestRepository.UpdateOne(request);
                _requestRepository.Save();

                return request;
            }
            else
            {
                throw new InputException("The request is no longer in pending authorization status");
            }

        }

        private Pharmacy UpdateStockInPharmacy(Pharmacy pharmacy, Request request)
        {
            if (request == null)
            {
                throw new InputException("Request empty");
            }
            
            if (pharmacy == null)
            {
                throw new InputException("Pharmacy empty");
            }

            foreach (Petition pet in request.Petitions)
            {
                foreach (Medicine med in pharmacy.Stock)
                {
                    if (med.Code.Contains(pet.MedicineCode))
                    {
                        med.Stock = med.Stock + pet.Amount;
                    }
                }
            }
            return pharmacy;
        }

        public void DeleteRequest(int requestId)
        {
            Pharmacy pharmacy = _pharmacyRepository.GetOneByExpression(p => p.Id == _sessionService.UserLogged.Pharmacy.Id);

            var requestStored = GetSpecificRequest(requestId);

            pharmacy.Requests.Remove(requestStored);
            ModifiedRecords(pharmacy, requestStored, true);
        }

        private List<Request> FilteredRequest(Pharmacy pharmacy, string state, List<Request> requests)
        {
            foreach (Request request in pharmacy.Requests)
            {
                if (request.State == state)
                {
                    requests.Add(request);
                }
            }
            return requests;
        }

        private void ModifiedRecords(Pharmacy pharmacy, Request request, bool delete)
        {
            if (delete)
            {
                _requestRepository.DeleteOne(request);
            }
            else
            {
                _requestRepository.InsertOne(request);
            }
            _requestRepository.Save();
            _pharmacyRepository.UpdateOne(pharmacy);
            _pharmacyRepository.Save();
        }

    }
}
