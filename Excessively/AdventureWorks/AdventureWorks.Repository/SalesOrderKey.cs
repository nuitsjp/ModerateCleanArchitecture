﻿namespace AdventureWorks.Repository
{
    public class SalesOrderKey : ISalesOrderKey
    {
        public SalesOrderKey(int value)
        {
            Value = value;
        }

        public int Value { get; }

    }
}