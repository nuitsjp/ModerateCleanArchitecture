using System.Threading.Tasks;
using AdventureWorks.Presentation;
using AdventureWorks.Repository;
using Microsoft.Data.SqlClient;
using KeyConverterProvider = AdventureWorks.Repository.KeyConverterProvider;

namespace AdventureWorks.App
{
    class Program
    {
        static async Task Main()
        {
            var connectionString = new SqlConnectionStringBuilder
            {
                DataSource = "localhost, 1444",
                UserID = "sa",
                Password = "P@ssw0rd!",
                InitialCatalog = "AdventureWorks"
            }.ToString();

            var consoleView =
                new ConsoleView(
                    new KeyConverterProvider(),
                    new KeyJsonConverterProvider(new KeyConverterProvider()),
                    new SalesOrderDetailRepository(
                        new SqlConnectionFactory(connectionString)));
            await consoleView.RunAsync();
        }
    }
}
