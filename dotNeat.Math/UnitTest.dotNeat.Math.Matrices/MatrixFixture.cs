using dotNeat.Math.Matrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.dotNeat.Math.Matrices
{
    [TestClass()]
    public class MatrixFixture
    {
        [TestMethod()]
        public void FactoryMethodsTest()
        {
            const ulong expectedHeight = 3;
            const ulong expectedWidth = 4;

            short[] rawPixels =
            {
                1,  2,  3,  4,
                5,  6,  7,  8,
                9, 10, 11, 12
            };

            var image = Matrix<short>.CreateMatrixWrapper(totalColumns: 4, totalRows: 3, rawMatrixData: rawPixels);

            Assert.IsNotNull(image);
            Assert.AreEqual(expectedHeight, image.Dimensions.Rows);
            Assert.AreEqual(expectedWidth, image.Dimensions.Columns);
            Assert.AreEqual(expectedWidth * expectedHeight, image.TotalEntries);
            Assert.AreEqual(12, image[2,3]);

            var imageClone = Matrix<short>.CreateMatrixClone(image);

            Assert.IsNotNull(imageClone);
            Assert.AreNotSame(image, imageClone);
            Assert.AreEqual(expectedHeight, imageClone.Dimensions.Rows);
            Assert.AreEqual(expectedWidth, imageClone.Dimensions.Columns);
            Assert.AreEqual(expectedWidth * expectedHeight, imageClone.TotalEntries);
            Assert.AreEqual(12, imageClone[2, 3]);

        }
    }
}