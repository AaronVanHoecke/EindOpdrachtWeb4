using Microsoft.EntityFrameworkCore;
using RestaurantDL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL
{
    public class RestaurantBeheerContext : DbContext
    {
        private string _connectionString = @"Data Source=WINDOWS-ISGC24U\SQLEXPRESS;Initial Catalog=RestaurantOpdracht;Integrated Security=True;TrustServerCertificate=True;";
        public DbSet<GebruikerEF> Gebruiker { get; set; }
        public DbSet<LocatieEF> Locatie { get; set; }
        public DbSet<RestaurantEF> Restaurant { get; set; }
        public DbSet<ReservatieEF> Reservatie { get; set; }
        public DbSet<TafelEF> Tafel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
