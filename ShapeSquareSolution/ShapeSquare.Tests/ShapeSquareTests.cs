using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShapeSquare.Tests
{
    [TestClass]
    public class ShapeSquareTests
    {
        CircleCreator circleCreator;
        TriangleCreator triangleCreator;

        public ShapeSquareTests()
        {
            circleCreator = CircleCreator.GetInstance();
            triangleCreator = TriangleCreator.GetInstance();
        }

        [TestMethod]
        [DataRow(10)]
        public void Circle_CalcSquare_Positive(int radius)
        {
            double square = Math.PI * Math.Pow(radius, 2);
            var shape = circleCreator.Create(radius);
            Assert.IsTrue(shape.CalcSquare() == square, "CalcSquare for circle returns wrong value");
        }

        [TestMethod]
        [DataRow(-10)]
        public void CircleCreator_Create_NegativeParam(int radius)
        {
            Assert.ThrowsException<ArgumentException>(() => circleCreator.Create(radius));
        }

        [TestMethod]
        [DataRow(40, 50, 60)]
        public void Triangle_CalcSquare_Positive(double a, double b, double c)
        {
            double perim = (a + b + c) / 2;
            double square = Math.Sqrt(perim * (perim - a) * (perim - b) * (perim - c));

            var shape = triangleCreator.Create(a, b, c);
            Assert.IsTrue(shape.CalcSquare() == square, "CalcSquare for triangle returns wrong value");
        }

        [TestMethod]
        [DataRow(-15, 25, 30, "Parameter can not be negative")]
        [DataRow(15, -25, 30, "Parameter can not be negative")]
        [DataRow(15, 25, -30, "Parameter can not be negative")]
        [DataRow(10, 20, 40, "Parameter values inconsistence")]
        [DataRow(10, 40, 20, "Parameter values inconsistence")]
        [DataRow(40, 10, 20, "Parameter values inconsistence")]
        public void TriangleCreator_Create_ParamsInconsistence(double a, double b, double c, string errMessage)
        {
            try
            {
                triangleCreator.Create(a, b, c);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, errMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }
    }
}