using AutoFixture;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ITFCode.Core.Domain.Tests.TestKit
{
    internal class DbContextCreator
    {
        #region Private Methods 

        private static readonly Fixture _fixture = new();

        #endregion

        public static TestDbContext CreateClear()
        {
            return CreateSqliteContext();
        }

        public static TestDbContext CreateFulled()
        {
            var dbContext = CreateClear();

            dbContext.Users.AddRange([.. DefaultData.Users]);
            dbContext.Roles.AddRange([.. DefaultData.Roles]);
            dbContext.UserRoles.AddRange([.. DefaultData.UserRoles]);
            dbContext.Locations.AddRange([.. DefaultData.Locations]);
            dbContext.ProductOrders.AddRange([.. DefaultData.ProductOrders]);

            dbContext.SaveChanges();

            foreach (var entry in dbContext.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            return dbContext;
        }

        #region MyRegion

        private static TestDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}")
                .Options;

            return new TestDbContext(options);
        }

        private static TestDbContext CreateSqliteContext()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite(CreateConnection()) // Use in-memory database for testing
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new TestDbContext(options);

            // Create the in-memory database and schema
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        private static SqliteConnection CreateConnection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            return connection;
        }

        #endregion
    }
}