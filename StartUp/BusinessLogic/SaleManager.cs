using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using StartUp.IBusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.BusinessLogic
{
    public class SaleManager : ISaleManager
    {
        public Sale CreateSale()
        {
            throw new NotImplementedException();
        }

        public List<Sale> GetAllSale(SaleSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public Sale GetSpecificSale()
        {
            throw new NotImplementedException();
        }
    }
}
