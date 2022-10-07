using StartUp.Domain;
using System.Collections.Generic;

namespace StartUp.IBusinessLogic
{
    public interface ISaleService
    {
        List<Sale> GetAllSale();
        Sale GetSpecificSale(int saleId);
        Sale CreateSale(Sale sale);
    }
}
