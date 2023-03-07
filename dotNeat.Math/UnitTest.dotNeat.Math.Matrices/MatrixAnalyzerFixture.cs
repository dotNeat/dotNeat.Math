using dotNeat.Math.Matrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.dotNeat.Math.Matrices
{
    [TestClass()]
    public class MatrixAnalyzerFixture
    {
        [TestMethod()]
        public void DetectAllDistinctEntrieValuesTest()
        {
            var rawPixels = new short[]
            {
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12
            };

            var image = Matrix<short>.CreateMatrixWrapper(totalColumns: 4, totalRows: 3, rawMatrixData: rawPixels);
            Assert.IsNotNull(image);
            var pixelValues = MatrixAnalyzer<short>.DetectAllDistinctEntrieValues(image);
            Assert.AreEqual(rawPixels.LongLength, pixelValues.LongCount());

            rawPixels = new short[]
            {
                1, 2, 3, 4,
                5,  6, 7, 8,
                9, 9, 9, 9,
            };
            image = Matrix<short>.CreateMatrixWrapper(totalColumns: 4, totalRows: 3, rawMatrixData: rawPixels);
            Assert.IsNotNull(image);
            pixelValues = MatrixAnalyzer<short>.DetectAllDistinctEntrieValues(image);
            Assert.AreEqual(9, pixelValues.LongCount());

            rawPixels = new short[]
            {
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
            };
            image = Matrix<short>.CreateMatrixWrapper(totalColumns: 4, totalRows: 3, rawMatrixData: rawPixels);
            Assert.IsNotNull(image);
            pixelValues = MatrixAnalyzer<short>.DetectAllDistinctEntrieValues(image);
            Assert.AreEqual(1, pixelValues.LongCount());

        }

        [TestMethod()]
        public void DetectEntrieRegionsTest()
        {
            var rawPixels = new short[]
            {
                1,1,0,0,0,
                0,1,0,0,1,
                1,0,0,1,1,
                0,0,0,0,0,
                0,0,0,0,0,
                1,0,1,0,1,
            };

            var image = Matrix<short>.CreateMatrixWrapper(totalColumns: 5, totalRows: 6, rawMatrixData: rawPixels);
            Assert.IsNotNull(image);

            var regions = MatrixAnalyzer<short>.DetectEntrieRegions(image, 1);
            Assert.AreEqual(5, regions.Count);
        }
    }
}