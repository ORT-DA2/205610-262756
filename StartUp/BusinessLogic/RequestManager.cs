using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class RequestManager : IRequestManager
    {
        public Request CreateRequest()
        {
            throw new NotImplementedException();
        }

        public List<Request> GetAllRequest(RequestSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Request GetSpecificRequest()
        {
            throw new NotImplementedException();
        }
    }
}
