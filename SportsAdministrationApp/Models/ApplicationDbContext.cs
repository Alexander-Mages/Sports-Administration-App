using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        //public DbSet<AthleteData> AthleteData { get; set; }
        //public DbSet<PRs> PRs { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<PersonalRecord> PersonalRecord { get; set; }
        public DbSet<AthleteData> AthleteData { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
    }
}
