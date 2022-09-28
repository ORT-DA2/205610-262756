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
            serviceCollection.AddScoped<IRepository<Employee>, EmployeeRepository>();
            serviceCollection.AddScoped<IRepository<Owner>, OwnerRepository>();
            serviceCollection.AddScoped<IRepository<Invitation>, InvitationRepository>();
            serviceCollection.AddScoped<IRepository<InvoiceLine>, InvoiceLineRepository>();
            serviceCollection.AddScoped<IRepository<Medicine>, MedicineRepository>();
            serviceCollection.AddScoped<IRepository<Petition>, PetitionRepository>();
            serviceCollection.AddScoped<IRepository<Pharmacy>, PharmacyRepository>();
            serviceCollection.AddScoped<IRepository<Request>, RequestRepository>();
            serviceCollection.AddScoped<IRepository<Sale>, SaleRepository>();
            serviceCollection.AddScoped<IRepository<Symptom>, SymptomRepository>();
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
            serviceCollection.AddTransient<ISessionLogic, SessionLogic>();
        }
    }
}