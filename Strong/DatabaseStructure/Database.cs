using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;

namespace DatabaseStructure
{
    public class Database
    {
        private static IDbConnectionFactory _connectionFactory = null!;

        public static void Initialize(IDbConnectionFactory dbConnectionFactory)
        {
            _connectionFactory = dbConnectionFactory;
        }

        internal static DbConnection Open() => _connectionFactory.Open();

        public async Task<IEnumerable<Schema>> AnalyzeAsync()
        {
            using var connection = Open();

            var schema = await connection.QueryAsync<Schema>(@"
select 
	schema_id as Id,
	name as Name,
	principal_id as PrincipalId
from 
	sys.schemas 
order by 
	name");
            var tables = await connection.GetSchemaAsync("Tables");


            var primaryKeys = await connection.QueryAsync(@"
");



            return null;
        }



        private static void DisplayData(System.Data.DataTable table)
        {
            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
                }
                Console.WriteLine("============================");
            }
        }
    }

    public record Table(string Catalog, string Schema, string Name, string Type);



    public record Schema(int Id, string Name, int PrincipalId);
}
