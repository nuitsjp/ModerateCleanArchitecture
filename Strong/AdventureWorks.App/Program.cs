using System.Threading;
using System.Threading.Tasks;
using AdventureWorks.Presentation;
using AdventureWorks.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdventureWorks.App
{
    class Program
    {
        static void Main()
        {
            CreateHostBuilder()
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ConsoleView>();
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
