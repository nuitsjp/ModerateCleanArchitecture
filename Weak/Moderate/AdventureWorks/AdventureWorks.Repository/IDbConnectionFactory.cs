using System.Data;

namespace AdventureWorks.Repository
{
    public interface IDbConnectionFactory
    {
        IDbConnection Open();
    }
}