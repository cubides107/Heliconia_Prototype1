using Heliconia.Domain.AccountingEntities;
using Heliconia.Domain.CategoryEntities;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.PurchasesEntities;
using Heliconia.Domain.UsersEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Infrastructure.EF
{
    public class HeliconiaContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<HeliconiaUser> HeliconiasUsers { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Estado> EstadosCategoria { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<DailyLedger> DailyLedgers { get; set; }

        public HeliconiaContext(DbContextOptions<HeliconiaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //nombre de esquema
            modelBuilder.HasDefaultSchema("Heliconia");

            //Configure entidades //companies
            modelBuilder.Entity<Company>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<Company>()
                        .Property(p => p.HeliconiaUserId)
                        .HasColumnType("varchar");


            //Configure entidades //stores
            modelBuilder.Entity<Store>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<Store>()
                        .Property(p => p.CompanyId)
                        .HasColumnType("varchar");

            //Configure entidades //heliconiauser
            modelBuilder.Entity<HeliconiaUser>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");


            //Configure entidades //manager
            modelBuilder.Entity<Manager>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<Manager>()
                        .Property(p => p.CompanyId)
                        .HasColumnType("varchar");


            //Configure entidades //worker
            modelBuilder.Entity<Worker>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<Worker>()
                        .Property(p => p.StoreId)
                        .HasColumnType("varchar");

            //Configure entidades //categoria
            modelBuilder.Entity<Category>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<Category>()
                        .Property(p => p.StoreId)
                        .HasColumnType("varchar");

            //Configure entidades //producto
            modelBuilder.Entity<Product>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<Product>()
                        .Property(p => p.CategoryElementId)
                        .HasColumnType("varchar");

            //Configure entidades //Estado
            modelBuilder.Entity<Estado>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<Estado>()
                        .Property(p => p.ProductId)
                        .HasColumnType("varchar");

            //Configure Entidades //Customer
            modelBuilder.Entity<Customer>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            //Configure Entidades //Purchase
            modelBuilder.Entity<Purchase>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<Purchase>()
                        .Property(p => p.CustomerId)
                        .HasColumnType("varchar");

            //Configure Entidades //Accounting
            modelBuilder.Entity<DailyLedger>()
                        .Property(p => p.Id)
                        .HasColumnType("varchar");

            modelBuilder.Entity<DailyLedger>()
                       .Property(p => p.CompanyId)
                       .HasColumnType("varchar");
        }
    }
}
