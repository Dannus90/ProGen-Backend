using System;
using System.Collections.Generic;
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

            var users = new List<User>()
            {
                new()
                {
                    Id = Guid.NewGuid(), Email = "testuser@gmail.com",
                    Password = "$2a$10$lmiYrmWUDf7klCsGo0VP.uI9DcK.5fUy2Ld34ahg8lQnIanlzThcy",
                    FirstName = "John", LastName = "Doe"
                }
            };

            modelBuilder.Entity<User>().HasData(users);
            
            //////////////////////////////////
            ///         SEED USERDATA      ///
            //////////////////////////////////
        
            var userData = new List<UserData>()
            {
                new()
                {
                    Id = Guid.NewGuid(), UserId = users[0].Id,
                    CityEn = "Gothenburg", CitySv = "Göteborg",
                    CountryEn = "Sweden", CountrySv = "Sverige",
                    PhoneNumber = "073-3249826", ProfileImage = "",
                    EmailCv = "persson.daniel.1990@gmail.com"
                }
            };

            modelBuilder.Entity<UserData>().HasData(userData);
        }
    }
}