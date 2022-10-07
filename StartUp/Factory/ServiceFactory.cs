using BusinessLogic;
using IBusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using StartUp.BusinessLogic;
using StartUp.IBusinessLogic;

namespace StartUp.Factory
{
    public class ServiceFactory
    {
        public void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAdministratorManager, AdministratorManager>();
            serviceCollection.AddTransient<IEmployeeManager, EmployeeManager>();
            serviceCollection.AddTransient<IOwnerManager, OwnerManager>();
            serviceCollection.AddTransient<IInvitationManager, InvitationManager>();
            serviceCollection.AddTransient<IInvoiceLineManager, InvoiceLineManager>();
            serviceCollection.AddTransient<IMedicineManager, MedicineManager>();
            serviceCollection.AddTransient<IPetitionManager, PetitionManager>();
            serviceCollection.AddTransient<IPharmacyManager, PharmacyManager>();
            serviceCollection.AddTransient<IRequestManager, RequestManager>();
            serviceCollection.AddTransient<ISaleManager, SaleManager>();
        }
    }
}