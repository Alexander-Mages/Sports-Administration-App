﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public new DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //seed database, going to keep this present to seed custom users
            modelBuilder.Entity<User>().HasData(
                            new User
                            {
                                //Id = 1,
                                Name = "John",
                                Team = "Swim",
                                Email = "John@gmail.com"
                            },
                            new User
                            {
                                //Id = 2,
                                Name = "Bill",
                                Team = "Tennis",
                                Email = "Bill@gmail.com"
                            }
                            );
        }
    }
}
