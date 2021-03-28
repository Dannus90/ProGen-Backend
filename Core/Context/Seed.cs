using System;
using Core.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public static class Seed
    {
        public static void SeedDataBase(ModelBuilder modelBuilder)
        {
            // Initial user with password password123
            
            //////////////////////////////////
            ///         SEED USER          ///
            //////////////////////////////////

            modelBuilder.Entity<User>().HasData(
                new User() {Id = Guid.NewGuid(), Email = "testuser@gmail.com", 
                    Password = "$2a$10$lmiYrmWUDf7klCsGo0VP.uI9DcK.5fUy2Ld34ahg8lQnIanlzThcy"});
        }
    }
}