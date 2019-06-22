using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FermView.Models;

namespace FermView.Models
{
    public class BrewsContext : DbContext
    {
       public BrewsContext(DbContextOptions<BrewsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemperatureData>();
        }


        public DbSet<TemperatureData> Temperatures { get; set; }


        public DbSet<FermView.Models.Brew> Brews { get; set; }


        public DbSet<FermView.Models.Profile> Profiles { get; set; }

        
    }
}
