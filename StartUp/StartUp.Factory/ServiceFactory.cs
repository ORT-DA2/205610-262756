using BusinessLogic;
using IBusinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartUp.BusinessLogic;
using StartUp.DataAccess;
using StartUp.DataAccess.Repositories;
using StartUp.Domain;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;

namespace StartUp.Factory
{
    public static class ServiceFactory
    {
        public static void RegisterDataAccessServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DbContext, StartUpContext>();
            serviceCollection.AddScoped<IRepository<Administrator>, AdministratorRepository>();
        }
        public static void RegisterServices(this IServiceCollection serviceCollection)
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