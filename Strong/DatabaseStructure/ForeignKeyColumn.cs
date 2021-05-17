namespace DatabaseStructure
{
    internal class ForeignKeyColumn
    {
        internal ForeignKeyColumn(Column parentColumn, Column referenceColumn)
        {
            ParentColumn = parentColumn;
            ReferenceColumn = referenceColumn;
        }

        internal Column ParentColumn { get; }
        internal Column ReferenceColumn { get; }
    }
}