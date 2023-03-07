namespace dotNeat.Math.Matrices
{
    using System;

    public record struct MatrixEntrie<TEntrieValue>
        where TEntrieValue : struct
    {
        internal MatrixEntrie(
            TEntrieValue value, 
            Matrix<TEntrieValue> matrix, 
            ulong row, 
            ulong column
            )
        {
            this.Value = value;
            this.Matrix = matrix;
            this.Location = new MatrixLocation(row: row, column: column);
        }

        public TEntrieValue Value { get; init; }
        public Matrix<TEntrieValue> Matrix { get; init; }
        public MatrixLocation Location { get; init; }
    }
}
