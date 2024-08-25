using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ITFCode.Core.Data.DataContext
{
    public abstract class ApplicationCoreDbContextFactory<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : ApplicationCoreDbContext
    {
        #region Private & Protected Fields

        private readonly string _connectionString = string.Empty;

        #endregion

        #region Constructors 

        public ApplicationCoreDbContextFactory(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be empty or null", nameof(connectionString));

            _connectionString = connectionString;
        }

        #endregion

        #region Public Methods: IDesignTimeDbContextFactory Implementation

        public TDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();

            // GetById config from appsettings.json
            ConfigurationBuilder builder = new();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // GetById connection string from appsettings.json
            string connectionString = config.GetConnectionString($"{_connectionString}");
            optionsBuilder.UseSqlServer(connectionString, opts =>
            {
                opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
            });

            var instance = Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);

            if (instance is null)
                throw new NullReferenceException($"Insance of '{typeof(TDbContext)}' could not be created");

            return (TDbContext)instance;
        }

        #endregion
    }
}