namespace dotNeat.Math.Matrices
{
    using System.Collections.Generic;

    public interface IMatrix<TEntrieValue> 
        where TEntrieValue : struct
    {
        MatrixDimensions Dimensions { get; }

        ulong TotalEntries { get; }
        
        TEntrieValue this[ulong row, ulong column] { get; internal set; }
        
        MatrixEntrie<TEntrieValue> GetMatrixEntrie(ulong row, ulong column);
        
        IReadOnlyCollection<MatrixEntrie<TEntrieValue>> GetAllMatrixEntries();
    }
}
