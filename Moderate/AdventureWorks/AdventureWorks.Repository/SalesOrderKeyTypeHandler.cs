using System.Data;
using Dapper;

namespace AdventureWorks.Repository
{
    public class SalesOrderKeyTypeHandler : SqlMapper.TypeHandler<SalesOrderKey>
    {
        public override SalesOrderKey Parse(object value)
        {
            return new((int)value);
        }

        public override void SetValue(IDbDataParameter parameter, SalesOrderKey value)
        {
            parameter.Value = value.AsPrimitive();
        }
    }
}