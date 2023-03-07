namespace dotNeat.Math.Matrices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public struct MatrixLocation
    {
        public MatrixLocation(ulong row, ulong column)
        {
            this.Row = row;
            this.Column = column;
        }

        public ulong Row { get; init; }
        public ulong Column { get; init; }
    }
}
