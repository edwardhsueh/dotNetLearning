using System;
using Packt;
using Xunit;
namespace CalculatorLibUnitTests
{
    public class CalculatorUnitTests
    {
        [Fact]
        public void TestAdding2And2()
        {
            // arrange
            decimal a = 2M;
            decimal b = 2M;
            decimal expected = 4M;
            var calc = new Calculator();
            // act
            decimal actual = calc.Add(a, b);

            // assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TestAdding2And3()
        {
            // arrange
            decimal a = 0.1M;
            decimal b = 0.2M;
            decimal expected = 0.3M;
            var calc = new Calculator();
            // act
            decimal actual = calc.Add(a, b);
            // assert
            Assert.Equal(expected, actual);
        }
    }
}
