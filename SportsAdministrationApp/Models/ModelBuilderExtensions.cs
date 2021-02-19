using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAdministrationApp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "1",
                    Name = "John",
                    Team = "Swim",
                    Email = "John@gmail.com",
                    UserName= "John@gmail.com"
                },
                new User
                {
                    Id = "2",
                    Name = "Bill",
                    Team = "Tennis",
                    Email = "Bill@gmail.com",
                    UserName= "Bill@gmail.com"
                }
            );
        }
    }
}
