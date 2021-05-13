using AdventureWorks.Presentation;
using AdventureWorks.Repository;
using AdventureWorks.UseCase.Export;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.App
{
    internal static class Program
    {
        private static void Main()
        {
            CreateHostBuilder()
                .RunConsoleAsync();
        }

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddHostedService<ConsoleView>();
                    services.AddTransient<IExportService, ExportService>();
                    services.AddTransient<ISalesOrderDetailRepository, SalesOrderDetailRepository>();
                    services.AddTransient<IDbConnectionFactory>(_ =>
                        new SqlConnectionFactory(
                            new SqlConnectionStringBuilder
                            {
                                DataSource = "localhost, 1444",
                                UserID = "sa",
                                Password = "P@ssw0rd!",
                                InitialCatalog = "AdventureWorks"
                            }.ToString()));
                });
    }
}
