using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FermViewApi.Models;

namespace FermViewApi.Models
{
    public class TemperatureDataContext : DbContext
    {
       public TemperatureDataContext(DbContextOptions<TemperatureDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TemperatureData>();
        }


        public DbSet<TemperatureData> Temperatures { get; set; }


        public DbSet<FermViewApi.Models.Brew> Brew { get; set; }

        
    }
}
