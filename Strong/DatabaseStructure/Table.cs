namespace DatabaseStructure
{
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
}