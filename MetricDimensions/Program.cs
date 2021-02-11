using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MetricDimensions
{
    class LibraryClass
    {
        // dimensions in a utility library
        private const string DatabaseIdDimension = "DatabaseId";
        public static DimensionNameTable MetricDimensionNames { get; } = new DimensionNameTable(DatabaseIdDimension);

        public void DoSomething(DimensionAccumulator parentDimensions)
        {
            parentDimensions.RecordDimension(DatabaseIdDimension, "MyDatabaseId");
        }
    }

    class Program
    {
        // the ambient dimensions
        private const string ClusterIdDimension = "ClusterId";
        private const string PodIdDimension = "PodId";
        private static DimensionNameTable MetricDimensionNames { get; } = new DimensionNameTable(LibraryClass.MetricDimensionNames, ClusterIdDimension, PodIdDimension);

        private static DimensionAccumulatorManager AccumulatorManager { get; } = new();

        static void Main(string[] args)
        {
            var lc = new LibraryClass();

            using var acc = AccumulatorManager.Get(MetricDimensionNames);
            acc.RecordDimension(ClusterIdDimension, "MyClusterId");
            acc.RecordDimension(PodIdDimension, "MyPodId");
            lc.DoSomething(acc);
        }
    }
}
