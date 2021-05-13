using System.Runtime.CompilerServices;

namespace AdventureWorks.Repository
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class KeyConverterInitializer
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            KeyConverterProvider.Register(new SalesOrderDetailKey.SalesOrderDetailKeyConverter());
        }
    }
}