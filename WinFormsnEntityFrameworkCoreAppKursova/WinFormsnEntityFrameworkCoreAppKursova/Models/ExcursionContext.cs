using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace WinFormsnEntityFrameworkCoreAppKursova.Models
{
    public class ExcursionContext : DbContext
    {
        
        public DbSet<Excursion> Excursions { get; set; }
        public DbSet<Bus> Buses { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<ExcursionType> ExcursionTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=DESKTOP-7S36ONQ\SQLEXPRESS;Database=DbExcursions;Trusted_Connection=True;");
        }
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
    }
}