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

            //seed teams here
            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    Id = 1,
                    Name = "Swim",
                    HeadCoach = "Coach1",
                    TeamCode = "Swim12345",
                    CoachCode = "coachcode1"
                },
                new Team
                {
                    Id = 2,
                    Name = "Tennis",
                    HeadCoach = "Coach2",
                    TeamCode = "Tennis12345",
                    CoachCode = "coachcode2"
                }
                );
        }
    }
}
