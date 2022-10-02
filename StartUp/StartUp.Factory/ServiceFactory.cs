using BusinessLogic;
using IBusinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StartUp.BusinessLogic;
using StartUp.DataAccess;
using StartUp.DataAccess.Repositories;
using StartUp.Domain;
using StartUp.Domain.Entities;
using StartUp.IBusinessLogic;
using StartUp.IDataAccess;

namespace StartUp.Factory
{
    public static class ServiceFactory
    {
        public static void RegisterDataAccessServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DbContext, StartUpContext>();
            serviceCollection.AddScoped<IRepository<User>, UserRepository>();
            serviceCollection.AddScoped<IRepository<Invitation>, InvitationRepository>();
            serviceCollection.AddScoped<IRepository<InvoiceLine>, InvoiceLineRepository>();
            serviceCollection.AddScoped<IRepository<Medicine>, MedicineRepository>();
            serviceCollection.AddScoped<IRepository<Petition>, PetitionRepository>();
            serviceCollection.AddScoped<IRepository<Pharmacy>, PharmacyRepository>();
            serviceCollection.AddScoped<IRepository<Request>, RequestRepository>();
            serviceCollection.AddScoped<IRepository<Sale>, SaleRepository>();
            serviceCollection.AddScoped<IRepository<Symptom>, SymptomRepository>();
            serviceCollection.AddScoped<IRepository<TokenAccess>, TokenRepository>();
            serviceCollection.AddScoped<IRepository<Session>, SessionRepository>();
            serviceCollection.AddScoped<IRepository<Role>, RoleRepository>();
        }
        public static void RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IInvitationService, InvitationService>();
            serviceCollection.AddTransient<IInvoiceLineService, InvoiceLineService>();
            serviceCollection.AddTransient<IMedicineService, MedicineService>();
            serviceCollection.AddTransient<IPetitionService, PetitionService>();
            serviceCollection.AddTransient<IPharmacyService, PharmacyService>();
            serviceCollection.AddTransient<IRequestService, RequestService>();
            serviceCollection.AddTransient<ISaleService, SaleService>();
            serviceCollection.AddTransient<ISymptomService, SymptomService>();
            serviceCollection.AddTransient<ISessionService, SessionService>();
            serviceCollection.AddTransient<ITokenAccessService, TokenAccessService>();
            serviceCollection.AddTransient<IRoleService, RoleService>();
        }
    }
}