using StartUp.Domain;
using StartUp.Domain.SearchCriterias;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartUp.IBusinessLogic
{
    public interface ISaleManager
    {
        List<Sale> GetAllSale(SaleSearchCriteria searchCriteria);
        Sale GetSpecificSale();
        Sale CreateSale();
    }
}
