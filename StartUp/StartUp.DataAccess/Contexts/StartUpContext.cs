using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StartUp.Domain;

namespace StartUp.DataAccess.Contexts
{
    public class StartUpContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Petition> Petitions { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Request> Requestes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        
        public StartUpContext(DbContextOptions options) : base(options) { }

        public StartUpContext() : base(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                var directory = Directory.GetCurrentDirectory();
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var conectionString = configuration.GetConnectionString("StartUpDb");
            } 
        }
    }
}