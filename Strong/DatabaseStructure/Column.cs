using System;

namespace DatabaseStructure
{
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
}