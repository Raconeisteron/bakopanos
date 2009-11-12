using System;
using System.Data;

namespace DotNetDataProviderTemplate
{
    internal class SampleDbResultSet
    {
        public struct MetaData
        {
            public string name;
            public Type type;
            public int maxSize;
        }

        public int recordsAffected;
        public MetaData[] metaData;
        public object[,] data;
    }
}