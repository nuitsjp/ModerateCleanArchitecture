using System;

namespace DatabaseStructure
{
    internal class DataType
    {
        internal DataType(string name, bool isUnsigned, Type type)
        {
            Name = name;
            IsUnsigned = isUnsigned;
            Type = type;
        }

        internal string Name { get; }
        internal bool IsUnsigned { get; }
        internal Type Type { get; }
    }
}