using System.Collections.Generic;

namespace dotNeat.Math.Matrices
{
    using System;
    using System.Diagnostics;


    public class Matrix<TEntrieValue> 
        : IMatrix<TEntrieValue> 
        where TEntrieValue : struct
    {
        private enum Dimension
        {
            Columns = 0,
            Rows = 1,
        }

        private readonly TEntrieValue[] _entries; // [row, column]
        private readonly ulong _totalEntries;
        private readonly MatrixDimensions _dimensions;

        #region factory methods

        public static Matrix<TEntrieValue>? CreateMatrixWrapper(
            ulong totalRows, 
            ulong totalColumns, 
            TEntrieValue[] rawMatrixData
            )
        {
            return new Matrix<TEntrieValue>(
                totalRows: totalRows,
                totalColumns: totalColumns,
                rawMatrixData: rawMatrixData
                );
        }
        
        public static Matrix<TEntrieValue>? CreateMatrixClone(
            ulong totalRows, 
            ulong totalColumns, 
            TEntrieValue[]? rawMatrixData = null
            )
        {
            TEntrieValue[]? rawImageDataClone = null;
            if (rawMatrixData is not null)
            {
                rawImageDataClone = new TEntrieValue[rawMatrixData.Length];
                rawMatrixData.CopyTo(rawImageDataClone, index: 0);
            }

            return new Matrix<TEntrieValue>(
                totalRows: totalRows,
                totalColumns: totalColumns,
                rawMatrixData: rawImageDataClone
                );
        }

        public static Matrix<TEntrieValue>? CreateMatrixClone(TEntrieValue[,] matrix)
        {
            return new Matrix<TEntrieValue>(matrix);
        }

        public static Matrix<TEntrieValue>? CreateMatrixClone(Matrix<TEntrieValue> matrix)
        {
            return new Matrix<TEntrieValue>(matrix);
        }

        #endregion factory methods

        #region private constructors

        private Matrix()
            : this(totalColumns: 0, totalRows: 0)
        {
        }

        private Matrix(Matrix<TEntrieValue> matrix)
            : this(
                totalRows: matrix.Dimensions.Rows,
                totalColumns: matrix.Dimensions.Columns 
                )
        {
            matrix._entries.CopyTo(this._entries, index:0);
        }

        private Matrix(TEntrieValue[,] pixelMatrix)
            : this(
                totalRows: Convert.ToUInt64(pixelMatrix.GetLongLength((int)Dimension.Rows)),
                totalColumns: Convert.ToUInt64(pixelMatrix.GetLongLength((int)Dimension.Columns))
                )
        {
            for (ulong row = 0; row < this._dimensions.Rows; row++)
            {
                for (ulong col = 0; col < this._dimensions.Columns; col++)
                {
                    this[row, col] = pixelMatrix[row, col];
                }
            }
        }

        private Matrix(
            ulong totalRows, 
            ulong totalColumns, 
            TEntrieValue[]? rawMatrixData = null
            )
        {
            _dimensions = new MatrixDimensions(totalRows, totalColumns);
            _totalEntries = Convert.ToUInt64(totalColumns * totalRows);
            _entries = rawMatrixData ?? new TEntrieValue[_totalEntries];

            Debug.Assert(Convert.ToUInt64(_entries.LongLength) == _totalEntries);
        }

        #endregion private constructors

        public MatrixDimensions Dimensions => _dimensions;
        public ulong TotalEntries => _totalEntries;

        public TEntrieValue this[ulong row, ulong column]
        {
            get => this._entries[(row * this._dimensions.Columns) + column];
            set => this._entries[(row * this._dimensions.Columns) + column] = value;
        }

        public MatrixEntrie<TEntrieValue> GetMatrixEntrie(ulong row, ulong column)
        {
            return new MatrixEntrie<TEntrieValue>(
                value: this[row, column], 
                matrix: this, 
                row: row, 
                column: column
                );
        }

        public IReadOnlyCollection<MatrixEntrie<TEntrieValue>> GetAllMatrixEntries()
        {
            List<MatrixEntrie<TEntrieValue>> results =
                new List<MatrixEntrie<TEntrieValue>>(Convert.ToInt32(this.TotalEntries));
            for (ulong row = 0; row < this.Dimensions.Rows; row++)
            {
                for (ulong col = 0; col < this.Dimensions.Columns; col++)
                {
                    results.Add(this.GetMatrixEntrie(row: row, column: col));
                }
            }
            return results;
        }

    }
}