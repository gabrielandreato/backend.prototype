using System.Data.Common;
using backend.person.datalibrary.DataContext;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace backend.person.test.integration;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<TestDataContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new MySqlConnection("DataSource=:memory:");
                connection.Open();
            
                return connection;
            });

            services.AddDbContext<TestDataContext>((container, options) =>
            {
                var connectionString = "";
                var connection = container.GetRequiredService<DbConnection>();
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        });

        builder.UseEnvironment("Development");
    }
}