using Xunit;
using HelperLibrary.Types;

namespace UnitTesting
{
    public class FractionTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 0)]
        public void Construct_Fraction_ShouldThrowError(uint numerator, uint denominator)
        {
            // Arrange
            Action createFraction = () => new Fraction(numerator, denominator);

            // Assert
            Assert.Throws<ArgumentException>(createFraction);
        }

        [Fact]
        public void Construct_Fraction_ShouldNotThrow()
        {
            // Arrange
            var exception = Record.Exception(() => new Fraction(0, 10));

            // Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 1, "1/1")]
        [InlineData(1, 2, 0.5)]
        public void CompareTo_NullableObject_ShouldThrow(uint numerator, uint denominator, object? value)
        {
            // Arrange
            Fraction fraction = new Fraction(numerator, denominator);
            Action compareFractionToNullableObject = () => fraction.CompareTo(value);

            // Assert
            Assert.Throws<ArgumentException>(compareFractionToNullableObject);
        }

        [Theory]
        [InlineData(1, 2, 1, 2, 0)]
        [InlineData(1, 2, 1, 3, 1)]
        [InlineData(1, 2, 1, 1, -1)]
        public void CompareTo_Fraction_ReturnsInteger(uint numerator, uint denominator, uint compareNumerator, uint compareDenominator, int expected)
        {
            // Arrange
            Fraction fraction = new Fraction(numerator, denominator);
            Fraction compareFraction = new Fraction(compareNumerator, compareDenominator);

            // Act
            int actual = fraction.CompareTo(compareFraction);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1, 1.0f)]
        [InlineData(2, 1, 2.0f)]
        [InlineData(1, 2, 0.5f)]
        [InlineData(0, 2, 0f)]
        [InlineData(1, 3, 1 / 3f)]
        public void ToSingle_Fraction_ReturnsSingle(uint numerator, uint denominator, float expected)
        {
            // Arrange
            Fraction fraction = new Fraction(numerator, denominator);

            // Act
            float actual = (float)fraction;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, true)]
        [InlineData(1, 1, 1, 2, false)]
        [InlineData(3, 2, 1, 2, false)]
        [InlineData(0, 2, 0, 5, true)]
        public void Equal_Fraction_ReturnBoolean(uint numerator, uint denominator, uint compareNumerator, uint compareDenominator, bool expected)
        {
            // Arrange
            Fraction fraction = new Fraction(numerator, denominator);
            Fraction compareFraction = new Fraction(compareNumerator, compareDenominator);

            // Act
            bool actual = fraction == compareFraction;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 1, 1, 1, false)]
        [InlineData(1, 1, 1, 2, false)]
        [InlineData(3, 2, 1, 2, false)]
        [InlineData(0, 2, 0, 5, false)]
        [InlineData(1, 3, 1, 2, true)]
        public void LessThan_Fraction_ReturnBoolean(uint numerator, uint denominator, uint compareNumerator, uint compareDenominator, bool expected)
        {
            // Arrange
            Fraction fraction = new Fraction(numerator, denominator);
            Fraction compareFraction = new Fraction(compareNumerator, compareDenominator);

            // Act
            bool actual = fraction < compareFraction;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
