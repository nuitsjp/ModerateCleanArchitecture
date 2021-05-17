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
        static async Task Main(string[] args)
        {
            var factory = new SqlConnectionFactory(
                new SqlConnectionStringBuilder
                {
                    DataSource = "localhost, 1444",
                    UserID = "sa",
                    Password = "P@ssw0rd!",
                    InitialCatalog = "AdventureWorks"
                }.ToString());

            var connection = factory.Open();

            var tables = connection
                .GetTables()
                .ToList();
        }
    }

    public class Table
    {
        public Table(string name, PrimaryKeyColumn[] primaryKey)
        {
            Name = name;
            PrimaryKey = primaryKey;
        }

        public string Name { get; }

        public PrimaryKeyColumn[] PrimaryKey { get; }

        public override string ToString() => Name;
    }

    public class PrimaryKeyColumn
    {
        public PrimaryKeyColumn(Column parentColumn, Column? referenceColumn)
        {
            ParentColumn = parentColumn;
            ReferenceColumn = referenceColumn;
        }

        public Column ParentColumn { get; set; }
        public Column? ReferenceColumn { get; }

        public override string ToString() => ParentColumn.ColumnName;
    }

    public class Column
    {
        public Column(string tableName, string columnName, Type dataType)
        {
            TableName = tableName;
            ColumnName = columnName;
            DataType = dataType;
        }

        public string TableName { get; }
        public string ColumnName { get; }
        public Type DataType { get; }
    }

    public class DataType
    {
        public DataType(string name, bool isUnsigned, Type type)
        {
            Name = name;
            IsUnsigned = isUnsigned;
            Type = type;
        }

        public string Name { get; }
        public bool IsUnsigned{ get; }
        public Type Type { get; }
    }

    public class ForeignKeyColumn
    {
        public ForeignKeyColumn(Column parentColumn, Column referenceColumn)
        {
            ParentColumn = parentColumn;
            ReferenceColumn = referenceColumn;
        }

        public Column ParentColumn { get; }
        public Column ReferenceColumn { get; }
    }

    public static class DatabaseStructureExtensions
    {
        private static Dictionary<string, DataType>? _dataTypes;

        public static Dictionary<string, DataType> GetDataTypes(this DbConnection connection)
        {
            return _dataTypes ??= connection.GetSchema("DataTypes")
                .AsDynamic()
                .Where(x => x.DataType is not null)
                .Select(x =>
                    new DataType(
                        (string)x.TypeName.ToLower(), 
                        (bool?)x.IsUnsigned ?? false,
                        (Type)Type.GetType(x.DataType)))
                .ToDictionary(
                    x => x.Name,
                    x => x)
                .AddWith("hierarchyid", new DataType("hierarchyid", false, typeof(SqlHierarchyId)))
                .AddWith("geography", new DataType("geography", false, typeof(SqlGeography)));
        }

        public static DataType GetDataType(this DbConnection connection, string typeName) =>
            connection.GetDataTypes()[typeName];

        private static Dictionary<(string, string), Column>? _columns;
        public static Dictionary<(string, string), Column> GetColumns(this DbConnection connection)
        {
            return _columns ??= connection.GetSchema("Columns")
                .AsDynamic()
                .Select(x => new Column(
                    $"[{x.TABLE_SCHEMA}].[{x.TABLE_NAME}]",
                    x.COLUMN_NAME,
                    GetDataType(connection, (string)x.DATA_TYPE).Type))
                .ToDictionary(
                    x => (x.TableName, x.ColumnName),
                    x => x);
        }

        public static Column GetColumn(this DbConnection connection, string tableName, string columnName) =>
            connection.GetColumns()[(tableName, columnName)];

        private static Dictionary<(string, string), ForeignKeyColumn>? _foreignKeyColumns;
        public static Dictionary<(string, string), ForeignKeyColumn> GetForeignKeyColumns(this DbConnection connection)
        {
            return _foreignKeyColumns ??= connection
                .Query(@"
select
	'[' + ParentSchema.name + '].[' + ParentTable.name + ']' as ParentTableName,
	ParentColumn.name as ParentColumnName,
	'[' + ReferenceSchema.name + '].[' + ReferenceTable.name + ']' as ReferenceTableName,
	ReferenceColumn.name as ReferenceColumnName
from 
	sys.foreign_keys
    inner join sys.foreign_key_columns
        on foreign_keys.object_id = foreign_key_columns.constraint_object_id
	inner join sys.tables as ParentTable
		on	foreign_key_columns.parent_object_id = ParentTable.object_id
	inner join sys.schemas as ParentSchema
		on	ParentTable.schema_id = ParentSchema.schema_id
	inner join sys.columns as ParentColumn
        on	foreign_key_columns.parent_object_id = ParentColumn.object_id 
		and	foreign_key_columns.parent_column_id = ParentColumn.column_id
	inner join sys.tables as ReferenceTable
		on	foreign_keys.referenced_object_id = ReferenceTable.object_id
	inner join sys.schemas as ReferenceSchema
		on	ReferenceTable.schema_id = ReferenceSchema.schema_id
	inner join sys.columns as ReferenceColumn
        on	foreign_key_columns.referenced_object_id = ReferenceColumn.object_id 
		and	foreign_key_columns.referenced_column_id = ReferenceColumn.column_id
order by
	ParentTableName,
	ParentColumnName")
                .Select(x => new ForeignKeyColumn(
                    connection.GetColumn((string)x.ParentTableName, (string)x.ParentColumnName),
                    connection.GetColumn((string)x.ReferenceTableName, (string)x.ReferenceColumnName)))
                .ToDictionary(
                    x => (x.ParentColumn.TableName, x.ParentColumn.ColumnName),
                    x => x);
        }

        public static ForeignKeyColumn? GetForeignKeyColumn(this DbConnection connection, string parentTableName, string parentColumnName)
        {
            if (connection.GetForeignKeyColumns().TryGetValue((parentTableName, parentColumnName), out var value))
            {
                return value;
            }

            return null;
        }

        public static IEnumerable<Table> GetTables(this DbConnection connection)
        {
            return connection.Query(@"
select
	'[' + schemas.name + '].[' + Tables.name + ']' as TableName,
	index_columns.key_ordinal as KeyOrdinal,
	columns.name AS ColumnName
from
    sys.tables
	inner join sys.schemas
		on	tables.schema_id = schemas.schema_id
    inner join sys.key_constraints
		on	Tables.object_id = key_constraints.parent_object_id 
		and	key_constraints.type = 'PK'
    inner join sys.index_columns
		on	key_constraints.parent_object_id = index_columns.object_id
        and	key_constraints.unique_index_id  = index_columns.index_id
    inner join sys.columns 
		on	index_columns.object_id = columns.object_id
        AND index_columns.column_id = columns.column_id
order by
	TableName, 
	KeyOrdinal")
                .GroupBy(
                    x => (string)x.TableName,
                    x => (ColumnName: (string)x.ColumnName, KeyOrdinal: (int)x.KeyOrdinal))
                .Select(x =>
                {
                    var tableName = x.Key;
                    var columns = x.OrderBy(column => column.KeyOrdinal).ToList();
                    return new Table(
                        tableName,
                        columns
                            .OrderBy(c => c.KeyOrdinal)
                            .Select(c => new PrimaryKeyColumn(
                                connection.GetColumn(tableName, c.ColumnName),
                                connection.GetForeignKeyColumn(tableName, c.ColumnName)?.ReferenceColumn))
                            .ToArray());
                });
        }

        public static IEnumerable<dynamic> AsDynamic(this DataTable table)
        {
            return table.AsEnumerable().Select(x =>
            {
                IDictionary<string, object?> dict = new ExpandoObject()!;
                foreach (DataColumn column in x.Table.Columns)
                {
                    var value = x[column];
                    if (value is System.DBNull) value = null;
                    dict.Add(column.ColumnName, value);
                }
                return (dynamic)dict;
            });
        }

        public static Dictionary<TKey, TValue> AddWith<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,
            TValue value) where TKey : notnull
        {
            dictionary[key] = value;
            return dictionary;
        }
    }
}
