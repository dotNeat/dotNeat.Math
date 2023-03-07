namespace dotNeat.Math.Matrices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MatrixEntrieRegion<TEntrieValue>
        where TEntrieValue : struct
    {
        private readonly List<MatrixLocation> _regionEntrieLocations;

        public MatrixEntrieRegion(TEntrieValue entrieValue)
            : this(entrieValue, Array.Empty<MatrixLocation>())
        {

        }

        public MatrixEntrieRegion(TEntrieValue entrieValue, IEnumerable<MatrixLocation> regionEntrieLocations)
        {
            this.EntrieValue = entrieValue;
            _regionEntrieLocations = new List<MatrixLocation>(regionEntrieLocations);
        }

        public TEntrieValue EntrieValue { get; init; }
        public IReadOnlyCollection<MatrixLocation> EntrieLocations => _regionEntrieLocations;
        public ulong TotalEntries => Convert.ToUInt64(this._regionEntrieLocations.LongCount());

        public MatrixEntrieRegion<TEntrieValue> AddRegionEntrieLocation(ulong row, ulong column)
        {
            return this.AddRegionEntrieLocation(new MatrixLocation(row: row, column: column));
        }

        public MatrixEntrieRegion<TEntrieValue> AddRegionEntrieLocation(MatrixLocation entrieLocation)
        {
            this._regionEntrieLocations.Add(entrieLocation);
            return this;
        }

    }
}
