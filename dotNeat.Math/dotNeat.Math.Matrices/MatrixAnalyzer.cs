namespace dotNeat.Math.Matrices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MatrixAnalyzer<TEntrieValue>
        where TEntrieValue : struct, IEquatable<TEntrieValue>//, IComparable<TEntrieValue>
    {
        public static IReadOnlyCollection<TEntrieValue> DetectAllDistinctEntrieValues(
            Matrix<TEntrieValue> matrix
            )
        {
            var disinctPixelValues = 
                new HashSet<TEntrieValue>(Convert.ToInt32(matrix.Dimensions.Columns * matrix.Dimensions.Rows));

            for (ulong row = 0; row < matrix.Dimensions.Rows; row++)
            {
                for (ulong col = 0; col < matrix.Dimensions.Columns; col++)
                {
                    disinctPixelValues.Add(matrix[row, col]);
                }
            }

            return disinctPixelValues;
        }

        public static IReadOnlyDictionary<TEntrieValue, IReadOnlyCollection<MatrixEntrieRegion<TEntrieValue>>> DetectEntrieRegions(
            Matrix<TEntrieValue> matrix
            )
        {
            var distintEntrieValues = DetectAllDistinctEntrieValues(matrix);

            var regionsByEntrieValue = 
                new Dictionary<TEntrieValue, IReadOnlyCollection<MatrixEntrieRegion<TEntrieValue>>>(distintEntrieValues.Count());

            foreach (var entrieValue in distintEntrieValues)
            {
                regionsByEntrieValue[entrieValue] = DetectEntrieRegions(matrix, entrieValue);
            }
            
            return regionsByEntrieValue;
        }

        public static IReadOnlyCollection<MatrixEntrieRegion<TEntrieValue>> DetectEntrieRegions(
            Matrix<TEntrieValue> matrix,
            TEntrieValue entrieValue
        )
        {
            // Reference:
            // https://www.geeksforgeeks.org/find-the-number-of-islands-using-dfs/
            // and https://www.geeksforgeeks.org/find-the-number-of-distinct-islands-in-a-2d-matrix/

            bool[,] visitedMatrixEntries = new bool[matrix.Dimensions.Rows, matrix.Dimensions.Columns];

            int capacity = Convert.ToInt32(matrix.Dimensions.Columns * matrix.Dimensions.Rows);
            var regions = new List<MatrixEntrieRegion<TEntrieValue>>(capacity);

            for (ulong row = 0; row < matrix.Dimensions.Rows; ++row)
            {
                for (ulong col = 0; col < matrix.Dimensions.Columns; ++col)
                {
                    if (matrix[row, col].Equals(entrieValue) && !visitedMatrixEntries[row, col])
                    {
                        // if the pixel with the value of interest and not visited yet,
                        // visit all pixels in this region
                        // and allocate new corresponding region object:
                        ProcessDepthFirst(
                            matrix, 
                            entrieValue, 
                            Convert.ToInt64(row), 
                            Convert.ToInt64(col), 
                            visitedMatrixEntries
                            );
                        regions.Add(new MatrixEntrieRegion<TEntrieValue>(entrieValue));
                    }
                }
            }

            return regions;
        }

        private const int totalNeighbores = 8;
        private static readonly int[] rowNeighbores = new[] { -1,-1,-1, 0, 0, 1, 1, 1 };
        private static readonly int[] colNeighbores = new[] { -1, 0, 1,-1, 1,-1, 0, 1 };
        private static void ProcessDepthFirst(
            Matrix<TEntrieValue> matrix, 
            TEntrieValue entrieValue, 
            long row, 
            long col,
            bool[,] visitedMatrixEntries
            )
        {
            visitedMatrixEntries[row, col] = true;

            // recur for all immidiate neighbor pixels:
            for (int n = 0; n < totalNeighbores; ++n)
            {
                if (NeedToProcess(
                        matrix, 
                        entrieValue, 
                        row + rowNeighbores[n], 
                        col + colNeighbores[n], 
                        visitedMatrixEntries
                        ))
                {
                    ProcessDepthFirst(
                        matrix, 
                        entrieValue, 
                        row + rowNeighbores[n], 
                        col + colNeighbores[n], 
                        visitedMatrixEntries
                        );
                }
            }
        }

        private static bool NeedToProcess(
            Matrix<TEntrieValue> matrix, 
            TEntrieValue entrieValue, 
            long row, 
            long col, 
            bool[,] visitedMatrixEntries
            )
        {
            return
                // pixel location is within the valid range:
                (row >= 0)  && (col >= 0) && 
                (row < Convert.ToInt64(matrix.Dimensions.Rows)) && (col < Convert.ToInt64(matrix.Dimensions.Columns))
                // entrieValue of interest:
                && matrix[Convert.ToUInt64(row), Convert.ToUInt64(col)].Equals(entrieValue)
                // pixel location was not viseted yet:
                && !visitedMatrixEntries[row, col]
                ;
        }


        #region DetectEntrieRegions_Mutating

        public static IReadOnlyCollection<MatrixEntrieRegion<TEntrieValue>> DetectEntrieRegions_Mutating(
            Matrix<TEntrieValue> matrix, 
            TEntrieValue entrieValue
            )
        {
            // Reference:
            // https://www.geeksforgeeks.org/find-the-number-of-islands-using-dfs/
            // and https://www.geeksforgeeks.org/find-the-number-of-distinct-islands-in-a-2d-matrix/

            var testMatrix = Matrix<TEntrieValue>.CreateMatrixClone(matrix);
            if (testMatrix is null)
            {
                return Array.Empty<MatrixEntrieRegion<TEntrieValue>>();
            }

            int capacity = Convert.ToInt32(testMatrix.Dimensions.Columns * testMatrix.Dimensions.Rows);
            var regions = new List<MatrixEntrieRegion<TEntrieValue>>(capacity);

            for (ulong row = 0; row < testMatrix.Dimensions.Rows; row++)
            {
                for (ulong col = 0; col < testMatrix.Dimensions.Columns; col++)
                {
                    if (testMatrix[row, col].Equals(entrieValue))
                    {
                        regions.Add(new MatrixEntrieRegion<TEntrieValue>(entrieValue));
                        ProcessDepthFirst(testMatrix, row, col, entrieValue);
                    }
                }
            }

            return regions;
        }

        private static void ProcessDepthFirst(Matrix<TEntrieValue> matrix, ulong row, ulong col, TEntrieValue entrieValue)
        {
            if ( //row < 0 || col < 0 ||
                row >= matrix.Dimensions.Rows || col >= matrix.Dimensions.Columns
                || !matrix[row, col].Equals(entrieValue)
               )
            {
                return;
            }

            matrix[row, col] = default;
            ProcessDepthFirst(matrix, row+1, col, entrieValue);
            ProcessDepthFirst(matrix, row-1, col, entrieValue);
            ProcessDepthFirst(matrix, row, col+1, entrieValue);
            ProcessDepthFirst(matrix, row, col-1, entrieValue);
            ProcessDepthFirst(matrix, row+1, col+1, entrieValue);
            ProcessDepthFirst(matrix, row-1, col-1, entrieValue);
            ProcessDepthFirst(matrix, row+1, col-1, entrieValue);
            ProcessDepthFirst(matrix, row-1, col+1, entrieValue);
        }

        #endregion DetectEntrieRegions_Mutating

    }
}
