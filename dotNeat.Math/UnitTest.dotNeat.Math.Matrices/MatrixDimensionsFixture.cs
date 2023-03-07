using dotNeat.Math.Matrices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.dotNeat.Math.Matrices
{
    [TestClass()]
    public class MatrixDimensionsFixture
    {
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("Empty Matrix 0x0 [RxC]", new MatrixDimensions(0, 0).ToString());
            Assert.AreEqual("Row Vector 1x3 [RxC]", new MatrixDimensions(1, 3).ToString());
            Assert.AreEqual("Column Vector 3x1 [RxC]", new MatrixDimensions(3, 1).ToString());
            Assert.AreEqual("Square Matrix 3x3 [RxC]", new MatrixDimensions(3, 3).ToString());
            Assert.AreEqual("Rectangular Matrix 3x4 [RxC]", new MatrixDimensions(3, 4).ToString());
            Assert.AreEqual("Rectangular Matrix 4x3 [RxC]", new MatrixDimensions(4, 3).ToString());
        }
    }
}