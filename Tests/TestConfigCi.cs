using System;

namespace Tests
{
    public static class TestConfig
    {
        public const string ConnectionStringCi = @"
                Host=postgres;
                Port=5432;
                Username=progen;
                Password=progen;
                Database=progenlocal;";
        
        public const string ConnectionString = @"
                Host=localhost;
                Port=5432;
                Username=progen;
                Password=progen;
                Database=progenlocal;";

        public static string getTestConnectionString()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                   == "TestCi" ? ConnectionStringCi : ConnectionString;
        }
    }
}