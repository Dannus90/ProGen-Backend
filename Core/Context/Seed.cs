using System;
using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public static class Seed
    {
        public static void SeedDataBase(ModelBuilder modelBuilder)
        {
            Console.WriteLine("Seeding will be placed here!");
        }
    }
}