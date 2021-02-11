using System;
using System.Collections.Generic;

namespace MetricDimensions
{
    public class DimensionNameTable
    {
        private readonly string[] _names;
        private readonly HashSet<string> _set = new();

        public DimensionNameTable(params string[] names)
        {
            _ = names ?? throw new ArgumentNullException(nameof(names));
            _names = names;

            // ensure uniqueness
            foreach (var n in names)
            {
                if (_set.Contains(n))
                {
                    throw new ArgumentException($"Duplicate dimension name {n}", nameof(names));
                }
                _set.Add(n);
            }
        }

        public DimensionNameTable(DimensionNameTable extra, params string[] names)
        {
            _ = extra ?? throw new ArgumentNullException(nameof(extra));
            _ = names ?? throw new ArgumentNullException(nameof(names));

            _names = new string[extra.AllNames.Length + names.Length];

            int index = 0;
            foreach (var n in extra.AllNames)
            {
                _names[index++] = n;

                if (_set.Contains(n))
                {
                    throw new ArgumentException($"Duplicate dimension name {n}", nameof(names));
                }
            }

            foreach (var n in names)
            {
                _names[index++] = n;

                if (_set.Contains(n))
                {
                    throw new ArgumentException($"Duplicate dimension name {n}", nameof(names));
                }
            }
        }

        internal bool Contains(string name) => _set.Contains(name);
        internal Span<string> AllNames => _names;
    }
}
