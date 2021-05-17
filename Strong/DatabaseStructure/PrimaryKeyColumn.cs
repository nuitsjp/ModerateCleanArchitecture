namespace DatabaseStructure
{
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
}