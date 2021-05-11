using System.Runtime.CompilerServices;

namespace AdventureWorks.Repository
{
    public class KeyConverterInitializer
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            KeyConverterProvider.Register(new SalesOrderDetailKeyConverter());
        }
    }
}