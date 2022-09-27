using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface IRequestManager
    {
        List<Request> GetAllRequest(RequestSearchCriteria searchCriteria);
        Request GetSpecificRequest(int requestId);
        Request CreateRequest(Request request);
        Request UpdateRequest(int requestId,Request requestUpdate);
        void DeleteRequest(int requestId);
    }
}
