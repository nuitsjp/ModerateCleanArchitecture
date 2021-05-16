using System.Data;
using System.Data.Common;

namespace DatabaseStructure
{
    public interface IDbConnectionFactory
    {
        DbConnection Open();
    }
}