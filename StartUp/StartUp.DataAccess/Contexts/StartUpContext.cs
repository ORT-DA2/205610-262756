using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StartUp.Domain;
using StartUp.Domain.Entities;

namespace StartUp.DataAccess
{
    public class StartUpContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Petition> Petitions { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Request> Requestes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<TokenAccess> TokenAccess { get; set; }
        
        public StartUpContext(DbContextOptions options) : base(options) { }

        public StartUpContext() : base() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("StartUpDb");
                optionsBuilder.UseSqlServer(connectionString);
            } 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<List<string>>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.ToView("View_BlogPostCounts");
                        eb.Property(v => v).HasColumnName("Rol");
                    });
        }
    }
}