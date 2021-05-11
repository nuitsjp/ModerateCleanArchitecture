using System.Threading.Tasks;
using AdventureWorks.Presentation;
using AdventureWorks.Repository;
using Microsoft.Data.SqlClient;

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
                    new KeySerializerProvider(),
                    new KeyConverterProvider(new KeySerializerProvider()),
                    new SalesOrderDetailRepository(
                        new SqlConnectionFactory(connectionString)));
            await consoleView.RunAsync();
        }
    }
}
