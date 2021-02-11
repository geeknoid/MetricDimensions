using System;
using System.Collections.Generic;

namespace MetricDimensions
{
    /// <summary>
    /// Accumulates memory dimensions to use when reporting a metric
    /// </summary>
    public class DimensionAccumulator : IDisposable
    {
        internal DimensionNameTable Names { get; set; }
        private readonly Dictionary<string, string> _dimensions = new();

        public void Dispose()
        {
            _dimensions.Clear();
            // TODO: return to the manager
        }

        public void RecordDimension(string name, string value)
        {
            if (Names.Contains(name))
            {
                _dimensions[name] = value;
            }
        }

        internal void ExtractDimensions(Span<string> values)
        {
            if (values.Length != Names.AllNames.Length)
            {
                throw new ArgumentException($"Output span needs to be {Names.AllNames.Length} elements long", nameof(values));
            }

            int index = 0;
            foreach (var n in Names.AllNames)
            {
                values[index++] = _dimensions[n];
            }
        }
    }
}
