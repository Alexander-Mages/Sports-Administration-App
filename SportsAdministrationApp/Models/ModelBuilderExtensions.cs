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
            //Seed users here
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    //Id = "1",
                    //AthleteDataId = 2,
                    TwoFactorEnabled = true,
                    Name = "John",
                    TeamCode = "Swim",
                    Email = "John@gmail.com",
                    UserName = "John@gmail.com"
                },
                new User
                {
                    // Id = "2",
                    //AthleteDataId = 1,
                    Name = "Bill",
                    TeamCode = "Tennis",
                    Email = "Bill@gmail.com",
                    UserName = "Bill@gmail.com"
                }
            );
            //seed teams here
            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    Id = 1,
                    Name = "Swim",
                    HeadCoach = "Mr. Foo",
                    TeamCode = "Swim12345",
                    CoachCode = "anothacode"
                },
                new Team
                {
                    Id = 2,
                    Name = "Tennis",
                    HeadCoach = "Mr. Bar",
                    TeamCode = "Tennis12345",
                    CoachCode = "code"
                }
                );
        }
    }
}
