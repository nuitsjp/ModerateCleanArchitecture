using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;

namespace DatabaseStructure.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = 
                new SqlConnectionStringBuilder
                {
                    DataSource = "localhost, 1444",
                    UserID = "sa",
                    Password = "P@ssw0rd!",
                    InitialCatalog = "AdventureWorks"
                }.ToString();

            var database = Database.Load(connectionString);
        }
    }
}
